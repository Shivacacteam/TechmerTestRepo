using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.File;
using TechmerVisionManager.Provider;
using TechmerVisionManager.Models;
using System.Data;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;

namespace TechmerVisionManager
{
    class Program
    {

        static void Main(string[] args)
        {

            AppMenu menu = new AppMenu();

            while (true)
            {
                menu.writeMenu();
                var userInput = Console.ReadLine();

                switch (userInput.ToLower())
                {
                    case "0":
                        return;
                    case "1":
                        SetCors();
                        break;
                    case "2":
                        MoveAssets();
                        break;
                    case "3":
                        DeleteAssets();
                        break;
                    case "4":
                        CopyProductTemplateFromProduction();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void CopyProductTemplateFromProduction()
        {
            ProductTemplateCopier ptCopy = new ProductTemplateCopier();

        }

        private static void DeleteAssets()
        {
            RuleType rule = GetRuleType();
            String UserName = GetUserName();
            Boolean deleteUserAccount = DeleteUserAccount(UserName);
            AssetDeleter assetDeleter = new AssetDeleter(rule, UserName, deleteUserAccount);
            assetDeleter.Run();
            PressAnyKey();
        }

        private static void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
        }

        private static Boolean DeleteUserAccount(String UserName)
        {
            Boolean ret = false;
            Console.Write("/r/nDelete User Account y/[n]: ");
            String input = Console.ReadLine().ToLower();
            if (String.IsNullOrEmpty(input))
            {
                input = "n";
            }
            if (input.Equals("y"))
            {
                ret = true;
            }
            return ret;
        }

        private static void MoveAssets()
        {
            String UserName = GetUserName();
            String[] assetArray = GetAssetTypes();
            RuleType rule = GetRuleType();
            AssetMover assetMover = new AssetMover(rule, UserName, assetArray);
            assetMover.Run();
            PressAnyKey();
        }

        private static string[] GetAssetTypes()
        {
            string[] assetArray;
            Console.Write("/r/nEnter Asset Type (Workspaces,Grids,Products)[All]: ");
            String assetTypeInput = Console.ReadLine().ToLower();
            if (assetTypeInput == String.Empty)
            {
                assetArray = new String[3] { "Workspaces", "Grids", "Products" };
            }
            else
            {
                assetArray = assetTypeInput.Split(',');
                for (int i = assetArray.Length; i > 0; i--)
                {
                    assetArray[i] = CapitalizeFirstLetter(assetArray[i]);

                }
            }

            return assetArray;
        }

        private static String CapitalizeFirstLetter(String value)
        {
            return String.Concat(value.Substring(0, 1).ToUpper(), value.Substring(1, value.Length - 1));
        }

        private static string GetUserName()
        {
            Console.Write("Enter userid (adam.edmonds@gmail.com): ");
            String UserInput = Console.ReadLine().ToLower();
            String UserName = "";
            if (UserInput == String.Empty)
            {
                UserName = "adam.edmonds@gmail.com";
            }

            return UserName;
        }

        private static void SetCors()
        {
            RuleType rule = GetRuleType();
            CORSSetter corsSetter = new CORSSetter(rule);
            corsSetter.Run();
            PressAnyKey();
        }

        private static RuleType GetRuleType()
        {
            Console.Write("Enter Rule Type (Debug,Production)[Debug]: ");
            String ruleTypeInput = Console.ReadLine().ToLower();
            RuleType setupRules = RuleType.Debug;
            switch (ruleTypeInput)
            {
                case "production":
                    setupRules = RuleType.Production;
                    break;
                default:
                    setupRules = RuleType.Debug;
                    break;
            }

            return setupRules;
        }


    }
}
