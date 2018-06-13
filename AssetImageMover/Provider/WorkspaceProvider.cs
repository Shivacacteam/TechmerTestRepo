using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechmerVisionManager.Models;
using System.Data.Linq;

namespace TechmerVisionManager.Provider
{
    class WorkspaceProvider
    {
        public static SqlConnection GetWorkspaceContext(RuleType ruleType)
        {
            SqlConnection ret;
            if (ruleType == RuleType.Production)
            {
                ret = new SqlConnection("Server=tcp:tpm-azuresql01.database.windows.net,1433;Database=test-ColorProject;User ID=tpmSQLAdmin@tpm-azuresql01;Password=Egpnmas#16;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
            else
            {
                //ret = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=TechmerColor;Integrated Security=SSPI;MultipleActiveResultSets=true");
                ret = new SqlConnection("Data Source=DESKTOP-RBIFU5T;Initial Catalog=TechmerVisionMemberDev; User ID=sa;Password=sa123;Integrated Security=SSPI;MultipleActiveResultSets=true");
            }
            ret.Open();
            return ret;
        }
    }
}
