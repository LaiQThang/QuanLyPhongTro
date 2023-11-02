using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels;
using System.Text;

namespace QuanLyPhongTro.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RoomManagementContext _context;


        public LogMiddleware(RequestDelegate next, RoomManagementContext room)
        {

            _next = next;
            _context = room;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var method = request.Method;
            var path = request.Path;
            var queryString = request.QueryString;
            var userAgent = request.Headers["User-Agent"];
            var ipAddress = context.Connection.RemoteIpAddress;

            //var logMessage = $"{DateTime.Now} - IP: {ipAddress}, Method: {method}, Path: {path}{queryString}, User-Agent: {userAgent}";

            // Ghi log vào tệp
            //File.AppendAllText(logFilePath, logMessage + Environment.NewLine, Encoding.UTF8);

            var model = new IpAddressModel(_context);
            var check = model.getIpAddress(Convert.ToString(ipAddress));
            if ( await check == false)
            {
                await model.AddIpAddress(Convert.ToString(ipAddress), Convert.ToString(request), method, path, Convert.ToString(queryString), userAgent);
            }

            await _next(context);
        }
    }
}
