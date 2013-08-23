using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColFilterToSQL
{
    public class AdvancedColumnFilterParser
    {
        public string ColumnName { get; set; }
        public DataType ColumnDataType { get; set; }

        public string SQLExpression { get; set; }
        public List<AdvancedColumnFilterParameter> ColumnFilterParameters;


        //Left/Right Parens, sqlData and unary flag
        private List<int> leftParen = new List<int>();          //Each offset zero based for left paren
        private List<int> rightParen = new List<int>();         //Each offset zero based for left paren
        private List<string> sqlData = new List<string>();      //Even numbers are sql statements. Odd are OR or AND statements
        private List<int> unaryFlag = new List<int>();          //0: nothing, 1: NOT, 2: <, 3: >, 4: <=, 5: >= 

        private int parenCount = 0;                             //Number of parens
        private int sqlCount = 0;
        private bool qAct = false;                              //True when processing a double quoted string
        private bool lastStrEnd = false;
        private string colFilterError = string.Empty;
        
        public AdvancedColumnFilterParser(string columnName, DataType columnDataType, string columnFilter)
        {

            ColumnName = columnName;
            ColumnDataType = columnDataType;
            ColumnFilterParameters = new List<AdvancedColumnFilterParameter>();

            //No Filter passed in. Just exit.
            if (columnFilter == string.Empty || columnFilter == "%" || columnFilter == "*")
                return;

            //Replace quotes, percent, asterisks and underscores.
            columnFilter = columnFilter.Replace("\'", "\"");
            columnFilter = columnFilter.Replace("%", "[%]");
            columnFilter = columnFilter.Replace("*", "%");
            columnFilter = columnFilter.Replace("_", "[_]");

            leftParen = new List<int>();          
            rightParen = new List<int>();         
            sqlData = new List<string>();      
            unaryFlag = new List<int>();          
            parenCount = 0;                             
            sqlCount = -1;
            qAct = false;                              
            lastStrEnd = false;

            //Begin Parsing
            NewSection();

            for (int fidx = 0; fidx < columnFilter.Length; fidx++)
            {
                switch (columnFilter.Substring(fidx, 1))
                {
                    case "(":
                        //Check if we are in a quoted string
                        if (qAct == false)
                        {
                            if (lastStrEnd == true)
                                NewSection();
                            
                            parenCount++;
                            leftParen[sqlCount]++;

                            if (unaryFlag[sqlCount] > 0)
                            {
                                //Unary Operator before a Left Paren is not allowed. 
                                throw new System.ArgumentException("Unary operator before a parenthesis not allowed", columnFilter.Substring(0, fidx));
                            }
                        }
                        else
                        {
                            sqlData[sqlCount] += columnFilter.Substring(fidx, 1);       //Inside a SQL Statement
                        }
                        break;

                    case ")":
                        if (qAct == false)
                        {
                            parenCount--;
                            rightParen[sqlCount]++;
                            if (parenCount < 0)
                            {
                                //Too many closed parens
                                throw new System.ArgumentException("Too many right parenthesis found", columnFilter.Substring(0, fidx));
                            }
                        }
                        else
                        {
                            sqlData[sqlCount] += columnFilter.Substring(fidx, 1);       //Inside an SQL
                        }
                        break;

                    case "\"":
                        if (qAct == false)
                        {
                            qAct = true;
                        }
                        else
                        {
                            qAct = false;
                            EndSection();                                               //End of SQL
                        }

                        break;

                    case " ":
                        if (qAct == false)
                        {
                            //Ignore extra white space
                            if (sqlData[sqlCount].Length > 0)
                                EndSection();
                        }
                        else
                        {
                            sqlData[sqlCount] += columnFilter.Substring(fidx, 1);       //Inside an SQL
                        }
                        break;

                    default:
                        if (lastStrEnd == true)
                        {
                            //We are at the start of a new string. Start a new Section
                            NewSection();
                        }

                        sqlData[sqlCount] += columnFilter.Substring(fidx, 1);       //Inside an SQL
                        break;
                }
            }

            //Build the SQL WHERE Clause

            string parameterName = string.Empty;
            int parameterNumber = 0;

            SQLExpression = "(";

            for (int fidx = 0; fidx < sqlData.Count; fidx++)
            {
                for (int pCnt = 0; pCnt < leftParen[fidx]; pCnt++)
                {
                    SQLExpression += "(";
                }

                if ((fidx % 2) == 0)
                {

                    //Create SQL Parameter
                    parameterName = "@" + ColumnName + "_" + parameterNumber.ToString();
                    parameterNumber ++;
                    ColumnFilterParameters.Add(new AdvancedColumnFilterParameter(parameterName, sqlData[fidx]));

                    //Create SQL Statement
                    switch (unaryFlag[fidx])
                    {
                        case 1:
                            SQLExpression += columnName + " NOT LIKE " + QuotedString(ColumnDataType, parameterName);
                            break;

                        case 2:
                            SQLExpression += columnName + " < " + QuotedString(ColumnDataType, parameterName);
                            break;

                        case 3:
                            SQLExpression += columnName + " > " + QuotedString(ColumnDataType, parameterName);
                            break;

                        case 4:
                            SQLExpression += columnName + " <= " + QuotedString(ColumnDataType, parameterName);
                            break;

                        case 5:
                            SQLExpression += columnName + " >= " + QuotedString(ColumnDataType, parameterName);
                            break;

                        default:
                            SQLExpression += columnName + " LIKE " + QuotedString(ColumnDataType, parameterName);
                            break;
                    }
                }
                else
                {
                    SQLExpression += " " + sqlData[fidx] + " ";

                }

                for (int pCnt = 0; pCnt < rightParen[fidx]; pCnt++)
                {
                    SQLExpression += ")";
                }

            }

            SQLExpression += ")";
        }

        //Sets up the next sql item in the nested list (if we have a nested list)
        private void NewSection()
        {
            sqlCount++;
            sqlData.Add("");
            unaryFlag.Add(0);
            leftParen.Add(0);
            rightParen.Add(0);
            lastStrEnd = false;
        }

        //Ends a Section (if we have a nested list)
        private void EndSection()
        {
            if (((sqlCount % 2) == 1) && (sqlData[sqlCount].ToUpper() != "OR") && (sqlData[sqlCount].ToUpper() != "AND"))
            {
                throw new System.ArgumentException("Expected AND/OR value between " + sqlData[sqlCount - 1].Replace("%", "*") + " and " + sqlData[sqlCount].Replace("%", "*") + " Note: White space in filters must be double quoted (&#34;test filter&#34;)");
            }
            else
            {
                switch (sqlData[sqlCount].ToUpper())
                {
                    case "NOT":
                        unaryFlag[sqlCount] = 1;
                        sqlData[sqlCount] = string.Empty;
                        break;
                                            
                    case "<":
                        unaryFlag[sqlCount] = 2;
                        sqlData[sqlCount] = string.Empty;
                        break;
                    
                    case ">":
                        unaryFlag[sqlCount] = 3;
                        sqlData[sqlCount] = string.Empty;
                        break;
                    
                    case "<=":
                        unaryFlag[sqlCount] = 4;
                        sqlData[sqlCount] = string.Empty;
                        break;

                    case ">=":
                        unaryFlag[sqlCount] = 5;
                        sqlData[sqlCount] = string.Empty;
                        break;

                    default:
                        lastStrEnd = true;
                        break;
                }
            }
        }

        private string QuotedString(DataType type, string value)
        {
            switch (type)
            {
                case DataType.String:
                case DataType.DateTime:
                case DataType.Date:
                    return "N'" + value + "'";
                
                case DataType.Number:
                case DataType.Boolean:
                default:
                    return value;
            }
        }

        public enum DataType
        {
            String,
            Number,
            DateTime,
            Date,
            Boolean
        }
    }

    public class AdvancedColumnFilterParameter
    {
        public string FilterName  { get; set; }
        public string FilterValue { get; set; }
        public AdvancedColumnFilterParameter() { }
        public AdvancedColumnFilterParameter(string name, string value) 
        {
            FilterName = name;
            FilterValue = value;
        }
    }
}
