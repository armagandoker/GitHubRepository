using System.Data.Common;
using Netsis.Framework.Persister.Cfg;
using Netsis.Framework.Persister.Database.Context;
using Netsis.Framework.Persister.Hibernate.Dialect;
using Netsis.Framework.Persister.Hibernate.Driver;
using Netsis.Framework.Persister.Hibernate.Provider;
using System.Collections.Generic;
using Netsis.Framework.Utils;
using System;
using Netsis.Rdl.Contracts.Config;
using Netsis.Rdl.Contracts.Entities;
using Netsis.Framework.Types;

namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers
{
    public class DbConfigurator
    {
        static DbConfigurator()
        {
            
        }

        /*private static string GetNetsisDatabaseConnectionString()
        {            
            var response = SsoClient.Instance().GetAppSettings(null, null);
            if (response.AppSettingsDTO == null || response.ResultCode != global::Netsis.Framework.SsoService.SsoContracts.Enums.ResultCodes.Success)
                throw new Exception("SSO Servis ayarları okunamadı. SSO servisinizin çalıştığından emin olun.");

            Dictionary<String, Object> mySettings = NToolkit.Serializer.JSON.Deserialize<Dictionary<String, Object>>(response.AppSettingsDTO.Settings);

            NReportConfig.Instance().NtfAddress = "net.tcp:" +  mySettings["NtfServiceAddr"].ToString();
            return mySettings["ConnectionString"].ToString();
            //return "Data Source=192.168.10.53;Initial Catalog=NETSIS;Persist Security Info=True;User ID=sa;Password=sapass";
        }*/


        public static void Configure()
        {
            var connectionString = NAppSettings.ReadSetting(NConstants.NetsisDBConnectionString, ""); //GetNetsisDatabaseConnectionString();
            var dbConBuilder = new DbConnectionStringBuilder {ConnectionString = connectionString};
            var dbType = dbConBuilder.ContainsKey("initial catalog") || dbConBuilder.ContainsKey("Initial Catalog")
                             ? NDbType.SqlServer
                             : NDbType.Oracle;
            var section = new NDALConfigurationSection
                              {
                                  ContextProvider = new ContextProviderElement
                                                        {
                                                            ContextLevel = NContextLevel.Custom,
                                                            CustomProvider =
                                                                typeof(SingleContextProvider).AssemblyQualifiedName
                                                        },
                                  UseExtensions = true,
                                  Database = new DatabaseElement
                                                 {
                                                     DbType = dbType,
                                                     UseSameCredentialForAllDBs = true
                                                 }
                              };

            section.Database.TurkishConversion = false;

            section.Database.Hibernate = new HibernateElement
                                             {
                                                 IgnoreValidateRowCount = true,
                                                 UseDefaultInterceptor = true
                                             };

            section.Database.ConnectionStrings = new ConnectionStringCollection
                                                     {
                                                         new ConnectionStringElement
                                                             {
                                                                 Name = "",
                                                                 ConnectionString = connectionString,
                                                                 Default = true
                                                             }
                                                     };

            if (section.Database.DbType == NDbType.SqlServer)
            {
                section.Database.Hibernate.HibernateProperties.Add(new HibernatePropertyElement
                                                                       {
                                                                           Name = "dialect",
                                                                           Value =
                                                                               typeof (NMsSql2008Dialect).
                                                                               AssemblyQualifiedName
                                                                       });
                section.Database.Hibernate.HibernateProperties.Add(new HibernatePropertyElement
                                                                       {
                                                                           Name = "connection.provider",
                                                                           Value =
                                                                               typeof (NetsisSqlProvider).
                                                                               AssemblyQualifiedName
                                                                       });
                section.Database.Hibernate.HibernateProperties.Add(new HibernatePropertyElement
                                                                       {
                                                                           Name = "connection.driver_class",
                                                                           Value =
                                                                               typeof (NSql2008Driver).
                                                                               AssemblyQualifiedName
                                                                       });
                section.Database.Hibernate.HibernateProperties.Add(new HibernatePropertyElement
                                                                       {
                                                                           Name = "connection.connection_string",
                                                                           Value =
                                                                               "Data Source=Server;Initial Catalog=Catalog;Persist Security Info=True;User ID=id;Password=pass"
                                                                       });
            }
            else
            {
                section.Database.Hibernate.HibernateProperties.Add(new HibernatePropertyElement
                                                                       {
                                                                           Name = "dialect",
                                                                           Value =
                                                                               typeof (NOracle10gDialect).
                                                                               AssemblyQualifiedName
                                                                       });
                section.Database.Hibernate.HibernateProperties.Add(new HibernatePropertyElement
                                                                       {
                                                                           Name = "connection.provider",
                                                                           Value =
                                                                               typeof (NetsisOracleProvider).
                                                                               AssemblyQualifiedName
                                                                       });
                section.Database.Hibernate.HibernateProperties.Add(new HibernatePropertyElement
                                                                       {
                                                                           Name = "connection.driver_class",
                                                                           Value =
                                                                               typeof (NOracleDriver).AssemblyQualifiedName
                                                                       });
                section.Database.Hibernate.HibernateProperties.Add(new HibernatePropertyElement
                                                                       {
                                                                           Name = "connection.connection_string",
                                                                           Value =
                                                                               "Data Source=Server;User ID=id;Password=pass"
                                                                       });
            }
            section.Database.Hibernate.HibernateProperties.Add(new HibernatePropertyElement
                                                                   {Name = "adonet.batch_size", Value = "30"});

            //section.Database.Hibernate.MappingSchemas.Add(new MappingSchemaElement{Schema = "{WF}",ReplaceWith = "NETSIS"});

            section.Database.Hibernate.MappingAssemblies.Add(new MappingAssemblyElement { Assembly = "Netsis.Rdl.Contracts" });
            NDALConfigurator.Configure(section);
        }
    }
}
