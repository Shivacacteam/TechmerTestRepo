using System;
using System.Data;
using System.Data.Linq;
using System.Data.Sql;
using System.Data.SqlClient;
using TechmerVisionManager.Provider;

namespace TechmerVisionManager
{
    public class ProductTemplateCopier
    {
        public ProductTemplateCopier()
        {
            /*
            String sql = "Select * From ProductTemplate";
            SqlConnection prodDbCon = WorkspaceProvider.GetWorkspaceContext(Models.RuleType.Production);
            SqlConnection devDbCon = WorkspaceProvider.GetWorkspaceContext(Models.RuleType.Debug);
            SqlDataAdapter prodAdapter = new SqlDataAdapter(sql, prodDbCon);
            SqlDataAdapter devAdapter = new SqlDataAdapter(sql, devDbCon);
            DataSet prodDs = new DataSet();
            DataSet devDs = new DataSet();
            prodAdapter.Fill(prodDs);
            devDs = prodDs.Clone();
            foreach (DataRow dr in prodDs.Tables[0].Rows)
            {
                devDs.Tables[0].ImportRow(dr);
            }

            // Create the SelectCommand.
            SqlCommand command = new SqlCommand("SELECT * FROM ProductTemplate " +
                "WHERE Image = @Image", devDbCon);

            // Add the parameters for the SelectCommand.
            command.Parameters.Add("@Image", SqlDbType.NVarChar, 4000);

            devAdapter.SelectCommand = command;

            command = new SqlCommand(
            "INSERT INTO ProductTemplate ([Title], [Image], [NumColors], [Active], [HasBackgroundImage], [BackgroundImage]) " +
            "VALUES (@Title, @Image, @NumColors, @Active, @HasBackgroundImage, @BackgroundImage)", devDbCon);

            // Add the parameters for the InsertCommand.
            command.Parameters.Add("@Title", SqlDbType.NVarChar, 4000);
            command.Parameters.Add("@Image", SqlDbType.NVarChar, 4000);
            command.Parameters.Add("@NumColors", SqlDbType.Int);
            command.Parameters.Add("@Active", SqlDbType.Bit);
            command.Parameters.Add("@HasBackGroundImage", SqlDbType.Bit);
            command.Parameters.Add("@BackgroundImage", SqlDbType.NVarChar, 4000);

            devAdapter.InsertCommand = command;
            Console.WriteLine("Records Imported:" + devAdapter.Update(devDs).ToString());
            */
            String table = "ProductTemplateColor";
            String sql = "Select * From " + table;
            using (SqlConnection connSource = WorkspaceProvider.GetWorkspaceContext(Models.RuleType.Production))
            using (SqlCommand cmd = connSource.CreateCommand())
            using (SqlBulkCopy bcp = new SqlBulkCopy(WorkspaceProvider.GetWorkspaceContext(Models.RuleType.Debug)))
            {
                bcp.DestinationTableName = table;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                //connSource.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    bcp.WriteToServer(reader);
                }
            }
        }

    }
}