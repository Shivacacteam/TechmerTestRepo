using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechmerVisionManager.Models;

namespace TechmerVisionManager.Provider
{
    class MembershipProvider
    {
        public static SqlConnection GetMembershipContext(RuleType ruleType)
        {
            SqlConnection ret;
            if (ruleType == RuleType.Production)
            {
                ret = new SqlConnection("Server=tcp:tpm-azuresql01.database.windows.net,1433;Database=test-ColorProjectMembership;User ID=tpmSQLAdmin@tpm-azuresql01;Password=Egpnmas#16;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
            else
            {
                //ret = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-WebApplication1-20150818102057;Integrated Security=True;MultipleActiveResultSets=true");
                ret = new SqlConnection("Data Source=DESKTOP-RBIFU5T;Initial Catalog=TechmerVisionAppDev;Integrated Security=True;MultipleActiveResultSets=true");
            }
            ret.Open();
            return ret;
        }
    }
}
