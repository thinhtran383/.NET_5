# User manual



Để chạy code, hãy làm theo các bước sau:

1. Đầu tiên, chạy file SQL.sql để tạo cơ sở dữ liệu. Hãy đảm bảo bạn chạy các câu lệnh SQL trong file theo thứ tự đã cho.

2. Sửa giá trị của `connectionString` trong file `ConnectionString.cs`. Bạn có thể tìm file này tại đường dẫn sau: `StudentManagementPlus/ConnectionString.cs`.

   ```csharp
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;

   namespace StudentManagementPlus {
       public class ConnectionString {
           public static string connectionString = @"Data Source=THINHTRAN\MSSQLSERVER02;Initial Catalog=quanlysinhvien;Integrated Security=True;";
       }
   }

3. Hãy thay thế *Data Source* bằng tên máy chủ của bạn. Ví dụ: nếu tên máy chủ của bạn là *MyServer*, thì *connectionString* sẽ trở thành:
  ```csharp
  public static string connectionString = @"Data Source=MyServer;Initial Catalog=quanlysinhvien;Integrated Security=True;";
