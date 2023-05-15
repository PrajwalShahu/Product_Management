using Spectre.Console;
using System.Data.SqlClient;
namespace Product_Management
{
    class Product
    {
        //SqlConnection conn = new SqlConnection("Data source=IN-B33K9S3; Initial Catalog=Expenses_Tracker;Integrated Security=true");

        public void AddProduct(SqlConnection conn)
        {
            try
            {
                Console.WriteLine("Enter Product Name");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Product Description");
                string description = Console.ReadLine();
                Console.WriteLine("Enter Products Quantity");
                int quantity = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Product Price");
                decimal price = Convert.ToInt32(Console.ReadLine());

                string query = "insert into Products (Name, Description, quantity, Price) values (@Name, @Description, @Quantity, @Price)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Product Added Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured: {ex.Message}");
            }
        }


        public void GetProduct(SqlConnection conn)
        {
            Console.WriteLine("Enter Product ID");
            int id = Convert.ToInt32(Console.ReadLine());

            string query = $"SELECT * FROM Products WHERE Id={id}";
            SqlCommand command = new SqlCommand(query, conn);

            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("                                Product                                                  ");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine("{0,-15} {1,-13} {2,-15} {3,-15} {4,-15}", "Id", "Name", "Description", "Quantity", "Price");
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write("{0,-15}", reader[i]);
                }
                Console.WriteLine();
            }
            reader.Close();
        }

        public void GetProducts(SqlConnection conn)
        {
            string query = $"SELECT * FROM Products";
            SqlCommand command = new SqlCommand(query, conn);

            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("                                Product                                                  ");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine("{0,-15} {1,-13} {2,-15} {3,-15} {4,-15}", "Id", "Name", "Description", "Quantity", "Price");
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write("{0,-15}", reader[i]);
                }
                Console.WriteLine();
            }
            reader.Close();
        }

        public void UpdateProduct(SqlConnection conn)
        {
            try
            {
                Console.WriteLine("Enter the Id of the Product to update:");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter the new Product Name:");
                string newname = Console.ReadLine();

                Console.WriteLine("Enter the new Product Description:");
                string newdescription = Console.ReadLine();

                Console.WriteLine("Enter the new Product quantity");
                int newquantity = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter Product Price");
                decimal newprice = Convert.ToInt32(Console.ReadLine());

                string query = $"UPDATE Products SET Name = @Name, Description = @Description, Quantity = @Quantity, Price = @Price WHERE id = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Name", newname);
                cmd.Parameters.AddWithValue("@Description", newdescription);
                cmd.Parameters.AddWithValue("@Quantity", newquantity);
                cmd.Parameters.AddWithValue("@Price", newprice);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Product Updated Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured: {ex.Message}");
            }
        }

        public void DeleteProduct(SqlConnection conn)
        {
            Console.WriteLine("Enter Product Id you want to delete");
            int id = Convert.ToInt16(Console.ReadLine());

            string query = $"delete from Products WHERE Id = {id}";
            SqlCommand cmd = new SqlCommand(query, conn);
            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
            {
                Console.WriteLine("Product deleted successfully!");
            }
            else
            {
                Console.WriteLine("Product not deleted.");
            }

        }


    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection("Data source=IN-B33K9S3; Initial Catalog=Expenses_Tracker;Integrated Security=true");
            conn.Open();

            Product products = new Product();
            int choice = 0;

            while (true)
            {

                AnsiConsole.Markup("[yellow]-------[/] ");
                AnsiConsole.Markup("[underline cyan1 bold]Welcome To Product Management Application[/] ");
                AnsiConsole.Markup("[yellow]-------[/] ");
                Console.WriteLine();
                AnsiConsole.MarkupLine("[plum2]1. Add new Product[/]");
                AnsiConsole.MarkupLine("[plum2]2. Get Product[/]");
                AnsiConsole.MarkupLine("[plum2]3. Get all Products[/]");
                AnsiConsole.MarkupLine("[plum2]4. Update Product[/]");
                AnsiConsole.MarkupLine("[plum2]5. Delete Product[/]");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Enter only numbers");
                    
                }
                switch (choice)
                {
                    case 1:
                        {
                            products.AddProduct(conn);
                            break;
                        }

                    case 2:
                        {
                            products.GetProduct(conn);
                            break;
                        }

                    case 3:
                        {
                            products.GetProducts(conn);
                            break;
                        }

                    case 4:
                        {
                            products.UpdateProduct(conn);
                            break;
                        }

                    case 5:
                        {
                            products.DeleteProduct(conn);
                            break;
                        }
                }
            }
            conn.Close();
        }
    }
}