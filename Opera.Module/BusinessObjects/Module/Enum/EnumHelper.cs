using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Reflection;

namespace Mikrobar.Module.BusinessObjects
{
    public class EnumHelper
    {
        public static void EnumToFunction()
        {
            try
            {                
                if (Session.DefaultSession != null && Session.DefaultSession.Connection != null)
                {
                    switch (Session.DefaultSession.Connection.ToString())
                    {
                        case "System.Data.SqlClient.SqlConnection":
                            EnumToMsSqlFunction();
                            break;
                        case "System.Data.OracleClient.OracleConnection":
                            EnumToOracleFunction();
                            break;
                        case "MySql.Data.MySqlClient.MySqlConnection":
                            EnumToMySqlFunction();
                            break;
                        default:
                            break;
                    }
                }                
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine("Enumlar fonksiyona cevrilirken hata!");
                System.Diagnostics.Debug.WriteLine(exc.StackTrace);
            }
        }

        static void EnumToOracleFunction()
        {
            try
            {
                System.Reflection.Assembly[] ass = AppDomain.CurrentDomain.GetAssemblies();
                foreach (System.Reflection.Assembly assembly in ass)
                {
                    if (assembly.FullName.StartsWith("Mikrobar.Module"))
                    {
                        Type[] tipler = assembly.GetTypes();
                        if (tipler != null && tipler.Length > 0)
                        {
                            foreach (Type t in tipler)
                            {
                                if (t.IsEnum == false) continue;

                                string[] names = Enum.GetNames(t);
                                Array val = Enum.GetValues(t);

                                StringBuilder sb = new StringBuilder();
                                sb.AppendFormat("CREATE OR REPLACE FUNCTION \"f_{0}\" (p_Enum IN NUMBER) ", t.Name);
                                sb.AppendLine();
                                sb.AppendLine("RETURN VARCHAR2 ");
                                sb.AppendLine("AS BEGIN ");
                                sb.AppendLine("	RETURN ");
                                sb.AppendLine();
                                sb.AppendLine("CASE");
                                int x = 0;
                                for (int i = 0; i < names.Length; i++)
                                {
                                    x = (int)Enum.Parse(t, names[i], true).GetHashCode();
                                    sb.AppendFormat("	WHEN p_Enum = {0} THEN '{1}' ", x, names[i]);
                                    sb.AppendLine();
                                }
                                sb.AppendLine("	ELSE '' ");

                                sb.AppendLine("END; ");
                                sb.AppendLine("	-- DBMS_OUTPUT.PUT_LINE('Navicat for Oracle'); ");
                                sb.AppendLine("END; ");
                                sb.AppendLine();

                                System.Diagnostics.Debug.WriteLine(sb.ToString());
                                Session.DefaultSession.ExecuteNonQuery(sb.ToString());
                            }
                        }
                    }
                }                
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.StackTrace);
            }
        }

        static void EnumToMsSqlFunction()
        {
            try
            {
                System.Reflection.Assembly[] ass = AppDomain.CurrentDomain.GetAssemblies();
                foreach (System.Reflection.Assembly assembly in ass)
                {
                    if (assembly.FullName.StartsWith("Mikrobar.Module"))
                    {
                        Type[] tipler = assembly.GetTypes();
                        if (tipler != null && tipler.Length > 0)
                        {
                            foreach (Type t in tipler)
                            {
                                if (t.IsEnum == false) continue;

                                string[] names = Enum.GetNames(t);
                                Array val = Enum.GetValues(t);

                                string drop = string.Format("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_{0}]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))", t.Name);
                                drop += Environment.NewLine;
                                drop += string.Format("DROP FUNCTION [dbo].[f_{0}]", t.Name);

                                StringBuilder sb = new StringBuilder();
                                sb.AppendFormat("CREATE FUNCTION [dbo].[f_{0}]", t.Name);
                                sb.AppendLine();
                                sb.AppendLine("(");
                                sb.AppendLine("	@DURUM INT");
                                sb.AppendLine(")");
                                sb.AppendLine("RETURNS NVARCHAR(16)");
                                sb.AppendLine("AS BEGIN");
                                sb.AppendLine("");
                                sb.AppendLine("RETURN");
                                sb.AppendLine("CASE");
                                int x = 0;
                                for (int i = 0; i < names.Length; i++)
                                {
                                    x = (int)Enum.Parse(t, names[i], true).GetHashCode();
                                    sb.AppendFormat("	WHEN @DURUM = {0} THEN N'{1}'", x, names[i]);
                                    sb.AppendLine();
                                }

                                sb.AppendLine("END ");
                                sb.AppendLine("END ");
                                sb.AppendLine();

                                System.Diagnostics.Debug.WriteLine(sb.ToString());
                                Session.DefaultSession.ExecuteNonQuery(drop);
                                Session.DefaultSession.ExecuteNonQuery(sb.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.StackTrace);
            }
        }

        static void EnumToMySqlFunction()
        {
            try
            {
                System.Reflection.Assembly[] ass = AppDomain.CurrentDomain.GetAssemblies();
                foreach (System.Reflection.Assembly assembly in ass)
                {
                    if (assembly.FullName.StartsWith("Mikrobar.Module"))
                    {
                        Type[] tipler = assembly.GetTypes();
                        if (tipler != null && tipler.Length > 0)
                        {
                            foreach (Type t in tipler)
                            {
                                if (t.IsEnum == false) continue;

                                string[] names = Enum.GetNames(t);
                                Array val = Enum.GetValues(t);

                                string drop = string.Format("DROP FUNCTION IF EXISTS f_{0};", t.Name);

                                StringBuilder sb = new StringBuilder();
                                sb.AppendFormat("CREATE FUNCTION `f_{0}`", t.Name);
                                sb.AppendLine();
                                sb.AppendLine("(");
                                sb.AppendLine("	pDURUM INT");
                                sb.AppendLine(")");
                                sb.AppendLine("RETURNS VARCHAR(16)");
                                sb.AppendLine("BEGIN");
                                sb.AppendLine("");
                                sb.AppendLine("RETURN");
                                sb.AppendLine("CASE");
                                int x = 0;
                                for (int i = 0; i < names.Length; i++)
                                {
                                    x = (int)Enum.Parse(t, names[i], true).GetHashCode();
                                    sb.AppendFormat("	WHEN pDURUM = {0} THEN N'{1}'", x, names[i]);
                                    sb.AppendLine();
                                }

                                sb.AppendLine("END; ");
                                sb.AppendLine("END; ");
                                sb.AppendLine();

                                System.Diagnostics.Debug.WriteLine(sb.ToString());
                                Session.DefaultSession.ExecuteNonQuery(drop);
                                Session.DefaultSession.ExecuteNonQuery(sb.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.StackTrace);
            }
        }
    }
}
