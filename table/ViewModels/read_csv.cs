using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using table.ViewModels;
using Microsoft.SqlServer;
using System.Data.SqlClient;
using System.Globalization;
using System.Diagnostics.SymbolStore;
using System.Data;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

class read_csv
{
    
    /// <summary>
    /// описание метода
    /// </summary>
    /// <param name="filePath">аргумент</param>
    /// <returns></returns>
    public static void ReadCsvFile(string filePath)
    {

        SqlConnection sqlConnection = new SqlConnection("Server=DESKTOP-I7FI92O\\SQLEXPRESS;Database=ex;Trusted_Connection=True;TrustServerCertificate=True;");
        sqlConnection.Open();
        SqlCommand sqlCommand = sqlConnection.CreateCommand();


        Dictionary<int, Book> books = new Dictionary<int, Book>();
        Dictionary<int, human> peoples = new Dictionary<int, human>();
        List<purchase> purchases = new List<purchase>();


        
        List<string[]> csvData = new List<string[]>();

        int book_id = 1;
        int people_id = 1;
        foreach (var row in File.ReadAllLines(filePath))
        {
            
            var kakhochesh = row.Split(','); 
            int for_books = Convert.ToInt32(kakhochesh[6]); //ключ для буксов
            int for_peoples = Convert.ToInt32(kakhochesh[0]);//ключ для человеков


            if (!books.ContainsKey(for_books))
            {
                books.Add((for_books), new Book() {ID = book_id, name = Convert.ToString(kakhochesh[5]), autor = Convert.ToString(kakhochesh[7]), article = for_books });
            } //проверка на одинаковые ячейки в буксах

            if (!peoples.ContainsKey(for_peoples))
            {
                peoples.Add((for_peoples), new human() { ID = people_id, ind_num = for_peoples, last_name = Convert.ToString(kakhochesh[1]), middle_name = Convert.ToString(kakhochesh[3]), name = Convert.ToString(kakhochesh[2]), sex = Convert.ToInt32(kakhochesh[4]) });
            } //проверка на одинаковые ячейки в пиполсах


            purchases.Add(new purchase() { Book = books[for_books], human = peoples[for_peoples], buy_date = DateTime.ParseExact(kakhochesh[8], "dd/MM/yyyy H:mm:ss", null)});

            book_id++;
            people_id++;
        }
        //return csvData;


        DataSet DS_book = new DataSet("ds_b");
        DataTable DT_book = new DataTable("book");
        DS_book.Tables.Add(DT_book); //add to dataset

        DataColumn ID_book = new DataColumn("ID", typeof(Int32));
        
        DataColumn name = new DataColumn("name", typeof(string));
        DataColumn autor = new DataColumn("autor", typeof(string));
        DataColumn article = new DataColumn("article", typeof(Int32));

        DT_book.Columns.Add(ID_book);
        DT_book.Columns.Add(name);
        DT_book.Columns.Add(autor);
        DT_book.Columns.Add(article);


        
        foreach (var Book in books.Values)
        {
            

            DataRow rowss = DT_book.NewRow();
            

            rowss[0] = Book.ID;
            rowss[1] = Book.name;
            rowss[2] = Book.autor;
            rowss[3] = Book.article;
            
            DT_book.Rows.Add(rowss);
        }
       

        DataTable DT_people = new DataTable("people");
        DS_book.Tables.Add(DT_people);
        DataColumn ID_people = new DataColumn("ID", typeof(Int32));

        DataColumn ind_num = new DataColumn("ind_num", typeof(Int32));
        DataColumn name_of_people = new DataColumn("name", typeof(string));
        DataColumn last_name = new DataColumn("last_name", typeof(string));
        DataColumn middle_name = new DataColumn("middle_name", typeof(string));
        DataColumn sex = new DataColumn("sex", typeof(Int32));
        DT_people.Columns.Add(ID_people);
        DT_people.Columns.Add(ind_num);
        DT_people.Columns.Add(name_of_people);
        DT_people.Columns.Add(last_name);
        DT_people.Columns.Add(middle_name);
        DT_people.Columns.Add(sex);

        foreach (var human in peoples.Values)
        {
            DataRow rowss = DT_people.NewRow();
            
            rowss[0] = human.ID;
            rowss[1] = human.ind_num;
            rowss[2] = human.name;
            rowss[3] = human.last_name;
            rowss[4] = human.middle_name;
            rowss[5] = human.sex;

            DT_people.Rows.Add(rowss);
        }


        

        DataTable DT_purchase = new DataTable("purchase");
        DS_book.Tables.Add(DT_purchase);
        DataColumn ID_purchase = new DataColumn("ID", typeof(Int32));
        DataColumn books_id = new DataColumn("book_ID", typeof(Int32));
        DataColumn peoples_id = new DataColumn("people_ID", typeof(Int32));
        DataColumn buy_date = new DataColumn("buy_date", typeof(DateTime));
        DT_purchase.Columns.Add(ID_purchase);
        DT_purchase.Columns.Add(books_id);
        DT_purchase.Columns.Add(peoples_id);
        DT_purchase.Columns.Add(buy_date);

        foreach (var purchase in purchases)
        {
            DataRow rowss = DT_purchase.NewRow();
            rowss[0] = purchase.ID;
            rowss[1] = purchase.Book.article;
            rowss[2] = purchase.human.ind_num;
            rowss[3] = purchase.buy_date;
            DT_purchase.Rows.Add(rowss);
        }


        using (var bulkinsert = new SqlBulkCopy(sqlConnection))
        {

            
            bulkinsert.DestinationTableName = DT_book.TableName;
            bulkinsert.WriteToServer(DT_book);

            
            bulkinsert.DestinationTableName = DT_people.TableName;
            bulkinsert.WriteToServer(DT_people);

            bulkinsert.DestinationTableName=DT_purchase.TableName;
            bulkinsert.WriteToServer(DT_purchase);


        }



    }

}