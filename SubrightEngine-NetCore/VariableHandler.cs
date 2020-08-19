using SubrightEngine.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine
{
    public class VariableHandler
    {
        //Handles all of the variables in such like interpreted languages etc
        public Dictionary<string, string> vars = new Dictionary<string, string>();

        public void AddVariable(string varName, string varData)
        {
            if (!varExists(varName))
            {
                //Variable does not exist to add it!
                vars.Add(varName, varData);
            }
            else
            {
                Debug.Error("Unable to create variable " + varName + " already exists");
            }
        }

        public void AddVariable(string varName, int varData)
        {
            //Create an integer variable
            AddVariable(varName, varData.ToString());
        }

        public void AddVariable(string varName, float varData)
        {
            //Create a float variable
            AddVariable(varName, varData.ToString());
        }

        public void AddVariable(string varName, double varData)
        {
            //Create a double variable
            AddVariable(varName, varData.ToString());
        }

        public void ReplaceVariable(string varName, string varData)
        {
            if (varExists(varName))
            {
                //Variable exists! so replace it!
                vars[varName] = varData;
            }
            else
            {
                Debug.Error("Unable to replace variable " + varName + " as it does not exist!");
            }
        }

        public void ReplaceVariable(string varName, int varData)
        {
            //Replace a integer value!
            ReplaceVariable(varName, varData.ToString());
        }

        public void ReplaceVariable(string varName, float varData)
        {
            //Replace a float value!
            ReplaceVariable(varName, varData.ToString());
        }

        public void ReplaceVariable(string varName, double varData)
        {
            //Replace a double value!
            ReplaceVariable(varName, varData.ToString());
        }

        public void RemoveVariable(string varName)
        {
            if (varExists(varName))
            {
                //Variable exists! so remove it.
                foreach(string m in vars.Keys)
                {
                    if(varName == m)
                    {
                        vars.Remove(m);
                    }
                }
            }
            else
            {
                Debug.Error("Unable to remove variable " + varName + " does not exist!");
            }
        }

        public string RetrieveValue(string varName)
        {
            //Retrieve a string value
            if (varExists(varName))
            {
                string retrievableValue = "";
                foreach (string m in vars.Keys)
                {
                    if(varName == m)
                    {
                        retrievableValue = m;
                    }
                }
                return retrievableValue;
            }
            else
            {
                Debug.Error("Unable to retrieve variable " + varName + " does not exist!");
                return null;
            }
        }

        public int RetrieveIntegerValue(string varName)
        {
            //retrieve a integer value!
            return int.Parse(RetrieveValue(varName));
        }

        public float RetrieveFloatValue(string varName)
        {
            //retrieve a float value!
            return float.Parse(RetrieveValue(varName));
        }

        public double RetrieveDoubleValue(string varName)
        {
            //retrieve a double value!
            return double.Parse(RetrieveValue(varName));
        }

        public bool RetrieveBoolValue(string varName)
        {
            return bool.Parse(varName);
        }

        public bool isValueBool(string varName)
        {
            bool finalAnswer = false;
            if (RetrieveValue(varName).Contains("true") || RetrieveValue(varName).Contains("false"))
            {
                finalAnswer = true;
            }
            return finalAnswer;
        }

        public bool isValueInt(string varName)
        {
            bool isNumeric = int.TryParse(RetrieveValue(varName), out _);
            return isNumeric;
        }

        //Checks for if the variable exists!
        public bool varExists(string varName)
        {
            bool varExists = false;
            foreach(string m in vars.Keys)
            {
                if(varName == m)
                {
                    varExists = true;
                }
            }
            return varExists;
        }
    }
}
