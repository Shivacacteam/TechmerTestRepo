using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechmerVisionManager.Models;

namespace TechmerVisionManager.Provider
{
    class AssetDeleter
    {
        RuleType _rule;
        String _userName;
        Boolean _deleteAccount;
        public AssetDeleter(RuleType rule, String UserName, Boolean DeleteAccount)
        {
            _rule = rule;
            _userName = UserName;
            _deleteAccount = DeleteAccount;
        }
        public void Run()
        {
            SqlConnection dbCon = WorkspaceProvider.GetWorkspaceContext(_rule);
            SqlCommand sqlCommand = new SqlCommand("Delete from Workspace Where UserId = " + MangerUtils.Quotes(_userName), dbCon);
            Console.WriteLine("User Assets Deleted: " + sqlCommand.ExecuteNonQuery().ToString());
            dbCon.Close();

            if (_deleteAccount)
            {
                dbCon = MembershipProvider.GetMembershipContext(_rule);
                sqlCommand = new SqlCommand("Delete from AspNetUsers Where Email = " + MangerUtils.Quotes(_userName), dbCon);
                Console.WriteLine("Users Records Deleted: " + sqlCommand.ExecuteNonQuery().ToString());
                dbCon.Close();

            }
        }
    }
}
