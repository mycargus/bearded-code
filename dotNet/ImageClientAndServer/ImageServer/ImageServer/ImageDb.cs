using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ServerCore;

namespace ImageServer
{
    public class ImageDb : Database
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly SqlConnection _connection;

        public ImageDb(string a_ConnectionString)
        {
            _connection = new SqlConnection(a_ConnectionString);
        }

        private SqlCommand CreateCommand(string a_CommandSqlText)
        {
            var command = _connection.CreateCommand();
            command.CommandText = a_CommandSqlText;
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 60; // seconds
            return command;
        }

        private void ExecuteQuery(string a_Query, ref DataTable a_Results)
        {
            using (_connection)
            {
                var command = CreateCommand(a_Query);
                _connection.Open();

                using (var adapter = new SqlDataAdapter(command))
                    adapter.Fill(a_Results);
            }
        }

        public override string GetCategoryList()
        {
            var query = GetCategoryListQuery();
            var results = new DataTable();

            try
            {
                ExecuteQuery(query, ref results);
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
                throw;
            }

            var list = ParseCategoryList(results);
            return list;
        }

        private static string ParseCategoryList(DataTable a_CategoriesTable)
        {
            var categoryList = new StringBuilder();

            try
            {
                categoryList.Append("<CategoryList>");

                foreach (DataRow row in a_CategoriesTable.Rows)
                {
                    var name = row["Category"].ToString();
                    var index = Convert.ToInt32(row["CIndex"]);
                    categoryList.Append(AddCategory(name, index));
                }

                categoryList.Append("</CategoryList>");
            }
            catch (Exception e)
            {
                _log.Error("ParseCategoryList() ERROR", e);
                throw;
            }

            return categoryList.ToString();
        }

        private static string AddCategory(string a_Name, int a_Index)
        {
            return String.Format("<Category name=\"{0}\" index=\"{1}\"/>", a_Name, a_Index);
        }

        public override string GetImageList(int a_Index)
        {
            var query = GetImageListQuery(a_Index);
            var results = new DataTable();

            try
            {
                ExecuteQuery(query, ref results);
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
                throw;
            }

            var list = ParseImageList(results);
            return list;
        }

        private static string ParseImageList(DataTable a_CategoriesTable)
        {
            var imageList = new StringBuilder();

            try
            {
                imageList.Append("<ImageList>");

                foreach (DataRow row in a_CategoriesTable.Rows)
                {
                    var name = row["FileName"].ToString();
                    var index = Convert.ToInt32(row["IIndex"]);
                    imageList.Append(AddImage(name, index));
                }

                imageList.Append("</ImageList>");
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
                throw;
            }

            return imageList.ToString();
        }

        private static string AddImage(string a_Name, int a_Index)
        {
            return String.Format("<Item name=\"{0}\" index=\"{1}\"/>", a_Name, a_Index);
        }

        public override byte[] GetImage(int a_Index)
        {
            var query = GetImageQuery(a_Index);
            var imageData = new byte[1024];

            try
            {
                ExecuteImageQuery(query, ref imageData);
            }
            catch (Exception e)
            {
                _log.Error(e.Message, e);
                throw;
            }

            return imageData;
        }

        private void ExecuteImageQuery(string a_Query, ref byte[] a_ImageData)
        {
            using (_connection)
            {
                var command = CreateCommand(a_Query);
                _connection.Open();

                using (var reader = command.ExecuteReader())
                    if (reader.Read())
                        a_ImageData = (byte[]) reader["Image"];
            }
        }

        private static string GetCategoryListQuery()
        {
            var builder = new StringBuilder();

            builder.Append("select CIndex, Category");
            builder.Append("from Categories");

            return builder.ToString();
        }

        private static string GetImageListQuery(int a_Index)
        {
            var builder = new StringBuilder();

            builder.Append("select i.IIndex, i.FileName");
            builder.Append("from Images i");
            builder.Append(String.Format("where i.CIndex = {0}", a_Index));

            return builder.ToString();
        }

        private static string GetImageQuery(int a_Index)
        {
            var builder = new StringBuilder();

            builder.Append("select Image");
            builder.Append("from Images");
            builder.Append(String.Format("where IIndex = {0}", a_Index));

            return builder.ToString();
        }

    }
}