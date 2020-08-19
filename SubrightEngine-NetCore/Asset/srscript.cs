using SubrightEngine.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubrightEngine.Asset
{
    public class srscript
    {
        //The Subright Engine Script Interpreter
        public void LoadFile(string file)
        {
            if (File.Exists(file))
            {
                //File exists
                string[] lines = File.ReadAllLines(file);
                RunScript(lines);
            }
            else
            {
                Debug.Error("File doesnt exist at: " + file);
            }
        }

        public srscript()
        {
            if(handler == null)
            {
                handler = new VariableHandler();
            }
        }

        public void RunScript(string[] code)
        {
            for(int i = 0; i < code.Length; i++)
            {
                string lineCode = code[i];
                RunScript(lineCode);
            }
        }

        //Var name, variable data
        public VariableHandler handler;

        public string afterScript(string[] args, int numbStart)
        {
            string finalString = "";
            if (args.Length < numbStart)
            {
                for (int i = numbStart; i < args.Length; i++)
                {
                    finalString = finalString + args[i];
                }
            }
            else
            {
                //Nothing dont do anything and pull an error
                Debug.Error("Script jumped ahead to a place where it isnt supposed to be");
            }
            return finalString;
        }

        public void RunScript(string code)
        {
            string[] args = code.Split(' ');
            switch (args[0].ToLower())
            {
                case "poggers":
                    Debug.Log("pogchamp");
                    break;
                case "forloop":
                    //Check for other variables to run out to
                    if (args.Length > 0 && args[1] != null && !args[1].Contains(""))
                    {
                        int numberruns = int.Parse(args[1]);
                        if (args.Length > 1 && args[2] != null && !args[2].Contains(""))
                        {
                            for (int i = 0; i < numberruns; i++)
                            {
                                RunScript(afterScript(args, 3));
                            }
                        }
                        else
                        {
                            Debug.Error("For loop doesnt have a valid command to run!");
                        }
                    }
                    else
                    {
                        //run erorr
                        Debug.Error("For loop doesnt have a number to run by");
                    }
                    break;
                case "while":
                    //get a while loop
                    if(args.Length > 0 && args[1] != null && !args[1].Contains(""))
                    {
                        //it would be a boolean if possible!
                        if (args.Length > 1 && args[2] != null && !args[1].Contains(""))
                        {
                            if (args[2].Contains("=="))
                            {
                                bool isBoolean = handler.isValueBool(args[1]);
                                bool firstBoolean = handler.RetrieveBoolValue(args[1]);
                                if (isBoolean)
                                {
                                    bool isOtherBool = handler.isValueBool(args[3]);
                                    if (isOtherBool == true)
                                    {
                                        //is a boolean against a boolean
                                        bool secondBoolean = handler.RetrieveBoolValue(args[3]);
                                        while (firstBoolean == secondBoolean)
                                        {
                                            RunScript(afterScript(args, 4));
                                        }
                                    }
                                    else
                                    {
                                        //Maybe its an integer
                                        bool isNumeric = handler.isValueInt(args[1]);
                                        if (isNumeric)
                                        {
                                            //is an boolean against a integer
                                            Debug.Error("Cannot compare a boolean to an integer! in a while loop");
                                        }
                                        else
                                        {
                                            //Maybe its just a string?
                                            //is an boolean against a string
                                            Debug.Error("Cannot compare a boolean to an string. in a while loop");
                                        }
                                    }
                                }
                                else
                                {
                                    //maybe an integer?
                                    bool isNumeric = handler.isValueInt(args[1]);
                                    int firstInteger = handler.RetrieveIntegerValue(args[1]);
                                    if (isNumeric)
                                    {
                                        bool isOtherBool = handler.isValueBool(args[3]);
                                        if (isOtherBool == true)
                                        {
                                            //Is a integer to a boolean
                                            Debug.Error("Cannot compare a integer to a boolean in a while loop!");
                                        }
                                        else
                                        {
                                            //Maybe its an integer
                                            bool isNumeric2 = handler.isValueInt(args[1]);
                                            if (isNumeric2)
                                            {
                                                //is a integer to a integer
                                                int secondInteger = handler.RetrieveIntegerValue(args[3]);
                                                while (firstInteger == secondInteger)
                                                {
                                                    RunScript(afterScript(args, 4));
                                                }
                                            }
                                            else
                                            {
                                                //Maybe its just a string?
                                                //integer to string
                                                Debug.Error("Cannot compare a integer to a string in a while loop!");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //Maybe its just a string?
                                        bool isOtherBool = handler.isValueBool(args[3]);
                                        string firstString = handler.RetrieveValue(args[1]);
                                        if (isOtherBool == true)
                                        {
                                            //string to boolean
                                            Debug.Error("Cannot compare a string to a boolean in a while loop!");
                                        }
                                        else
                                        {
                                            //Maybe its an integer
                                            bool isNumeric3 = handler.isValueInt(args[1]);
                                            if (isNumeric3)
                                            {
                                                //string to integer
                                                Debug.Error("Cannot compare a string to a integer in a while loop!");
                                            }
                                            else
                                            {
                                                //Maybe its just a string?
                                                //string to string
                                                string secondValue = handler.RetrieveValue(args[3]);
                                                while (firstString == secondValue)
                                                {
                                                    RunScript(afterScript(args, 4));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (args[2].Contains("!="))
                            {
                                bool isBoolean = handler.isValueBool(args[1]);
                                bool firstBoolean = handler.RetrieveBoolValue(args[1]);
                                if (isBoolean)
                                {
                                    bool isOtherBool = handler.isValueBool(args[3]);
                                    if (isOtherBool == true)
                                    {
                                        //is a boolean against a boolean
                                        bool secondBoolean = handler.RetrieveBoolValue(args[3]);
                                        while (firstBoolean != secondBoolean)
                                        {
                                            RunScript(afterScript(args, 4));
                                        }
                                    }
                                    else
                                    {
                                        //Maybe its an integer
                                        bool isNumeric = handler.isValueInt(args[1]);
                                        if (isNumeric)
                                        {
                                            //is an boolean against a integer
                                            Debug.Error("Cannot compare a boolean to an integer! in a while loop");
                                        }
                                        else
                                        {
                                            //Maybe its just a string?
                                            //is an boolean against a string
                                            Debug.Error("Cannot compare a boolean to an string. in a while loop");
                                        }
                                    }
                                }
                                else
                                {
                                    //maybe an integer?
                                    bool isNumeric = handler.isValueInt(args[1]);
                                    int firstInteger = handler.RetrieveIntegerValue(args[1]);
                                    if (isNumeric)
                                    {
                                        bool isOtherBool = handler.isValueBool(args[3]);
                                        if (isOtherBool == true)
                                        {
                                            //Is a integer to a boolean
                                            Debug.Error("Cannot compare a integer to a boolean in a while loop!");
                                        }
                                        else
                                        {
                                            //Maybe its an integer
                                            bool isNumeric2 = handler.isValueInt(args[1]);
                                            if (isNumeric2)
                                            {
                                                //is a integer to a integer
                                                int secondInteger = handler.RetrieveIntegerValue(args[3]);
                                                while (firstInteger != secondInteger)
                                                {
                                                    RunScript(afterScript(args, 4));
                                                }
                                            }
                                            else
                                            {
                                                //Maybe its just a string?
                                                //integer to string
                                                Debug.Error("Cannot compare a integer to a string in a while loop!");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //Maybe its just a string?
                                        bool isOtherBool = handler.isValueBool(args[3]);
                                        string firstString = handler.RetrieveValue(args[1]);
                                        if (isOtherBool == true)
                                        {
                                            //string to boolean
                                            Debug.Error("Cannot compare a string to a boolean in a while loop!");
                                        }
                                        else
                                        {
                                            //Maybe its an integer
                                            bool isNumeric3 = handler.isValueInt(args[1]);
                                            if (isNumeric3)
                                            {
                                                //string to integer
                                                Debug.Error("Cannot compare a string to a integer in a while loop!");
                                            }
                                            else
                                            {
                                                //Maybe its just a string?
                                                //string to string
                                                string secondValue = handler.RetrieveValue(args[3]);
                                                while (firstString != secondValue)
                                                {
                                                    RunScript(afterScript(args, 4));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Just check the state of the other one or split on what it has
                            string finalVar = args[1].Remove('!');
                            bool isBoolean = handler.isValueBool(args[1]);
                            if (isBoolean == true)
                            {
                                //Is a boolean
                                bool gottenHandler = handler.RetrieveBoolValue(args[1]);
                                if (args[1].Contains("!"))
                                {
                                    //Not
                                    while (!gottenHandler)
                                    {
                                        //Execute here
                                        Debug.Log("Single statements for while loops are not properly implemented yet!");
                                    }
                                }
                                else
                                {
                                    //Yes
                                    while (gottenHandler)
                                    {
                                        //Execute here
                                        Debug.Log("Single statements for while loop sare not properly implemented yet!");
                                    }
                                }
                            }
                            else
                            {
                                bool isNumeric = handler.isValueInt(args[1]);
                                if(isNumeric == true)
                                {
                                    //Is an integer
                                    Debug.Error("Cannot compare a integer in a while loop");
                                }
                                else
                                {
                                    //Maybe just be a string then!
                                    Debug.Error("Cannot compare a string, in a while loop");
                                }
                            }
                        }
                    }
                    break;
                case "if":
                    //get a while loop
                    if (args.Length > 0 && args[1] != null && !args[1].Contains(""))
                    {
                        //it would be a boolean if possible!
                        if (args.Length > 1 && args[2] != null && !args[1].Contains(""))
                        {
                            if (args[2].Contains("=="))
                            {
                                bool isBoolean = handler.isValueBool(args[1]);
                                bool firstBoolean = handler.RetrieveBoolValue(args[1]);
                                if (isBoolean)
                                {
                                    bool isOtherBool = handler.isValueBool(args[3]);
                                    if (isOtherBool == true)
                                    {
                                        //is a boolean against a boolean
                                        bool secondBoolean = handler.RetrieveBoolValue(args[3]);
                                        if (firstBoolean == secondBoolean)
                                        {
                                            RunScript(afterScript(args, 4));
                                        }
                                    }
                                    else
                                    {
                                        //Maybe its an integer
                                        bool isNumeric = handler.isValueInt(args[1]);
                                        if (isNumeric)
                                        {
                                            //is an boolean against a integer
                                            Debug.Error("Cannot compare a boolean to an integer in a if statement");
                                        }
                                        else
                                        {
                                            //Maybe its just a string?
                                            //is an boolean against a string
                                            Debug.Error("Cannot compare a boolean to an string in a if statement");
                                        }
                                    }
                                }
                                else
                                {
                                    //maybe an integer?
                                    bool isNumeric = handler.isValueInt(args[1]);
                                    int firstInteger = handler.RetrieveIntegerValue(args[1]);
                                    if (isNumeric)
                                    {
                                        bool isOtherBool = handler.isValueBool(args[3]);
                                        if (isOtherBool == true)
                                        {
                                            //Is a integer to a boolean
                                            Debug.Error("Cannot compare a integer to a boolean in a if statement");
                                        }
                                        else
                                        {
                                            //Maybe its an integer
                                            bool isNumeric2 = handler.isValueInt(args[1]);
                                            if (isNumeric2)
                                            {
                                                //is a integer to a integer
                                                int secondInteger = handler.RetrieveIntegerValue(args[3]);
                                                if (firstInteger == secondInteger)
                                                {
                                                    RunScript(afterScript(args, 4));
                                                }
                                            }
                                            else
                                            {
                                                //Maybe its just a string?
                                                //integer to string
                                                Debug.Error("Cannot compare a integer to a string in a if statement");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //Maybe its just a string?
                                        bool isOtherBool = handler.isValueBool(args[3]);
                                        string firstString = handler.RetrieveValue(args[1]);
                                        if (isOtherBool == true)
                                        {
                                            //string to boolean
                                            Debug.Error("Cannot compare a string to a boolean in a if statement");
                                        }
                                        else
                                        {
                                            //Maybe its an integer
                                            bool isNumeric3 = handler.isValueInt(args[1]);
                                            if (isNumeric3)
                                            {
                                                //string to integer
                                                Debug.Error("Cannot compare a string to a integer in a if statement");
                                            }
                                            else
                                            {
                                                //Maybe its just a string?
                                                //string to string
                                                string secondValue = handler.RetrieveValue(args[3]);
                                                if (firstString == secondValue)
                                                {
                                                    RunScript(afterScript(args, 4));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (args[2].Contains("!="))
                            {
                                bool isBoolean = handler.isValueBool(args[1]);
                                bool firstBoolean = handler.RetrieveBoolValue(args[1]);
                                if (isBoolean)
                                {
                                    bool isOtherBool = handler.isValueBool(args[3]);
                                    if (isOtherBool == true)
                                    {
                                        //is a boolean against a boolean
                                        bool secondBoolean = handler.RetrieveBoolValue(args[3]);
                                        if (firstBoolean != secondBoolean)
                                        {
                                            RunScript(afterScript(args, 4));
                                        }
                                    }
                                    else
                                    {
                                        //Maybe its an integer
                                        bool isNumeric = handler.isValueInt(args[1]);
                                        if (isNumeric)
                                        {
                                            //is an boolean against a integer
                                            Debug.Error("Cannot compare a boolean to an integer in a if statement");
                                        }
                                        else
                                        {
                                            //Maybe its just a string?
                                            //is an boolean against a string
                                            Debug.Error("Cannot compare a boolean to an string in a if statement");
                                        }
                                    }
                                }
                                else
                                {
                                    //maybe an integer?
                                    bool isNumeric = handler.isValueInt(args[1]);
                                    int firstInteger = handler.RetrieveIntegerValue(args[1]);
                                    if (isNumeric)
                                    {
                                        bool isOtherBool = handler.isValueBool(args[3]);
                                        if (isOtherBool == true)
                                        {
                                            //Is a integer to a boolean
                                            Debug.Error("Cannot compare a integer to a boolean in a if statement");
                                        }
                                        else
                                        {
                                            //Maybe its an integer
                                            bool isNumeric2 = handler.isValueInt(args[1]);
                                            if (isNumeric2)
                                            {
                                                //is a integer to a integer
                                                int secondInteger = handler.RetrieveIntegerValue(args[3]);
                                                if (firstInteger != secondInteger)
                                                {
                                                    RunScript(afterScript(args, 4));
                                                }
                                            }
                                            else
                                            {
                                                //Maybe its just a string?
                                                //integer to string
                                                Debug.Error("Cannot compare a integer to a string in a if statement");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //Maybe its just a string?
                                        bool isOtherBool = handler.isValueBool(args[3]);
                                        string firstString = handler.RetrieveValue(args[1]);
                                        if (isOtherBool == true)
                                        {
                                            //string to boolean
                                            Debug.Error("Cannot compare a string to a boolean in a if statement");
                                        }
                                        else
                                        {
                                            //Maybe its an integer
                                            bool isNumeric3 = handler.isValueInt(args[1]);
                                            if (isNumeric3)
                                            {
                                                //string to integer
                                                Debug.Error("Cannot compare a string to a integer in a if statement");
                                            }
                                            else
                                            {
                                                //Maybe its just a string?
                                                //string to string
                                                string secondValue = handler.RetrieveValue(args[3]);
                                                if (firstString != secondValue)
                                                {
                                                    RunScript(afterScript(args, 4));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Just check the state of the other one or split on what it has
                            string finalVar = args[1].Remove('!');
                            bool isBoolean = handler.isValueBool(args[1]);
                            if (isBoolean == true)
                            {
                                //Is a boolean
                                bool gottenHandler = handler.RetrieveBoolValue(args[1]);
                                if (args[1].Contains("!"))
                                {
                                    //Not
                                    if (!gottenHandler)
                                    {
                                        //Execute here
                                    }
                                }
                                else
                                {
                                    //Yes
                                    if (gottenHandler)
                                    {
                                        //Execute here
                                    }
                                }
                            }
                            else
                            {
                                bool isNumeric = handler.isValueInt(args[1]);
                                if (isNumeric == true)
                                {
                                    //Is an integer
                                    Debug.Error("Cannot compare a integer in a if statement");
                                }
                                else
                                {
                                    //Maybe just be a string then!
                                    Debug.Error("Cannot compare a string, in a if statement");
                                }
                            }
                        }
                    }
                    break;
                case "var":
                    if(args.Length > 0 && args[1] != null && !args[1].Contains(""))
                    {
                        if(args.Length > 1 && args[2] != null && !args[2].Contains(""))
                        {
                            //Add the product.
                            handler.AddVariable(args[1], args[2]);
                        }
                        else
                        {
                            Debug.Error("Argument does not define anything!");
                        }
                    }
                    else
                    {
                        Debug.Error("Variable doesnt have a name to start with!");
                    }
                    break;
                case "changevar":
                    if (args.Length > 0 && args[1] != null && !args[1].Contains(""))
                    {
                        if (args.Length > 1 && args[2] != null && !args[2].Contains(""))
                        {
                            //Add the product.
                            handler.ReplaceVariable(args[1], args[2]);
                        }
                        else
                        {
                            Debug.Error("Argument does not define anything!");
                        }
                    }
                    else
                    {
                        Debug.Error("Cannot change variable data as it doesnt have a name to start with!");
                    }
                    break;
                case "removevar":
                    if (args.Length > 0 && args[1] != null && !args[1].Contains(""))
                    {
                        handler.RemoveVariable(args[1]);
                    }
                    else
                    {
                        Debug.Error("Cannot remove variable as it doesnt have a name to start with!");
                    }
                    break;
                case "stop":
                    Debug.Log("Stopping...");
                    Application.Exit();
                    break;
                default:
                    Debug.Error("No command defined!");
                    break;
            }
        }
    }
}
