using System;
using System.Collections.Generic;
using System.Configuration;
using ModelLibrary;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace SQLConnectionRepository
{
    
    public static class SQLRepo
    {
        public static string GetConnectionStrings()
        {
            return @"Data Source=.\SQLEXPRESS;Initial Catalog=Task;Integrated Security=True";
        }
    }

    public class DataRepository
    {

        public bool  InsertRecordforMunicipality(MunicipalityDisplayModel mode)
        {

            return true;
        }


        public async Task<IEnumerable<MunicipalityDisplayModel>> GetDetails()
        {
            IEnumerable<MunicipalityDisplayModel> result = null;

            List<MunicipalityDisplayModel> modelresult = null;

            DataTable dtinfo = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(SQLRepo.GetConnectionStrings()))
                {
                    SqlDataAdapter adp = new SqlDataAdapter("SELECT TOP (100) MunicipalityTaxScheduled.DetailsId, " +
                        "MunicipalityTaxScheduled.MunicipalityId, MunicipalityTaxScheduled.TaxId, MunicipalityTaxScheduled.TaxType," +
                        " MunicipalityTaxScheduled.Date as [Date], MunicipalityTaxScheduled.Result,MunicipalityTaxScheduled.UpdatedDateTime," +
                        " MunicipalityTaxScheduled.DetailsId AS Expr1, MunicipalityTaxScheduled.MunicipalityId AS Expr2," +
                        " MunicipalityTaxScheduled.TaxId AS Expr3, MunicipalityTaxScheduled.TaxType AS Expr4, MunicipalityTaxScheduled.Date AS Expr5," +
                        " MunicipalityTaxScheduled.Result AS Expr6, MunicipalityTaxScheduled.UpdatedDateTime AS Expr7, MunicipalityInfo.Municipality_Name as MNAme," +
                        " TaxTypeRule.TaxName, TaxTypeRule.Result AS Expr8 FROM     MunicipalityTaxScheduled INNER JOIN " +
                        "  MunicipalityInfo ON MunicipalityTaxScheduled.MunicipalityId = MunicipalityInfo.Municipality_Id INNER JOIN" +
                        " TaxTypeRule ON MunicipalityTaxScheduled.TaxId = TaxTypeRule.TaxId ORDER BY MunicipalityTaxScheduled.UpdatedDateTime DESC", con);
                    
                    adp.Fill(dtinfo);

                    if(dtinfo.Rows.Count> 0)
                    {
                        foreach (DataRow row in dtinfo.Rows)
                        {
                            modelresult = new List<MunicipalityDisplayModel>
                            {
                                new MunicipalityDisplayModel{Id= Convert.ToInt32(row["DetailsId"].ToString()),
                                Name= row["MNAme"].ToString(),
                                ReleaseDate =Convert.ToDateTime(row["Date"]),
                                Result = row["Result"].ToString()}                               
                            };
                        }
                        result = modelresult.AsEnumerable();
                    }


                }
            }
            catch(Exception ex)
            {
                throw ;
            }

            return result;
        }

        
    }
}
