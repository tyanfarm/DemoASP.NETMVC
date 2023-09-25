using System.Net;

namespace WebApplication2.ExtendMethods
{
    public static class AppExtend
    {
        public static void AddStatusCodePage(this IApplicationBuilder app)
        {
            // cách để đăng ký middleware
            app.UseStatusCodePages(appError =>
            {
                // middleware chạy mỗi khi có lỗi HTTP
                // dùng async để cho phép await bên trong
                appError.Run(async context =>
                {
                    // tạo 1 tham chiếu tới đối tượng HttpResponse từ context
                    var response = context.Response;

                    // Lấy mã trạng thái từ response
                    var code = response.StatusCode;

                    var content = @$"
                        <html>
                            <head>
                                <meta charset='UTF-8' />
                                <title>Error {code} </title>
                            </head>
                            <body>
                                <p style='color: violet; font-size: 30px'> 
                                    Error: {code} - {(HttpStatusCode)code}
                                </p>
                            </body>
                        </html>";

                    // gửi nội dung HTML về client
                    await response.WriteAsync(content);
                });
            });
        }
    }
}
