using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NCore;
using sample01.Models;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace sample01.Controllers.v1
{
    [Produces("application/json")]//Controller中方法默认输出json格式数据
    //[Produces("application/xml")]//Controller中方法默认输出xml格式数据
    [Route("api/v1/testapi")]//固定路由配置
    //[Route("api/v1/[controller]")]//固定部分路由配置
    public class TestApiController : Controller
    {

        /// <summary>
        /// 返回用户信息接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]//请求方法为GET
        [Route("getuser")]//自定义路由
        //[Produces("application/xml")]//输出xml格式
        public Task<UserViewMmodel> Index()
        {
            return Task.Run(() =>
            {
                UserViewMmodel res = null;
                //业务逻辑代码....
                res = new UserViewMmodel
                {
                    address = "上海市浦东区世纪大道200号",
                    age = 23,
                    creation_time = DateTime.Now,
                    version = "v1.0"
                };

                return res;
            });
        }

        /// <summary>
        /// 输出文件接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]//请求方法为GET
        [Route("getfile")]//自定义路由
        public FileResult Download()
        {
            var fileData = $"{Directory.GetCurrentDirectory()}/wwwroot/css/site.css";
            var actionresult = new FileStreamResult(fileData.GetFileData().ToStream(), "text/css");
            actionresult.FileDownloadName = "site.css";
            return actionresult;
        }

        /// <summary>
        /// 裁剪图片接口
        /// </summary>
        /// <param name="width"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]//请求方法为GET
        [Route("getimage/{width}/{name}")]//自定义路由
        public IActionResult GetImage(int width, string name)
        {
            var imgPath = $@"{Directory.GetCurrentDirectory()}/wwwroot/imgs/{name}";

            //缩小图片
            using (var imgBmp = new Bitmap(imgPath))
            {
                //找到新尺寸
                var oWidth = imgBmp.Width;
                var oHeight = imgBmp.Height;
                var height = oHeight;
                if (width > oWidth)
                {
                    width = oWidth;
                }
                else
                {
                    height = width * oHeight / oWidth;
                }
                var newImg = new Bitmap(imgBmp, width, height);
                newImg.SetResolution(72, 72);
                byte[] bytes;
                using (var ms = new MemoryStream())
                {
                    newImg.Save(ms, ImageFormat.Bmp);
                    bytes = ms.GetBuffer();
                }
                return new FileContentResult(bytes, "image/jpeg");
            }
        }

        /// <summary>
        /// 上传文件,需要指定name值为fromFile，如：form-data; name="fromFile"; filename="8.png"
        /// </summary>
        /// <param name="fromFile"></param>
        /// <returns></returns>
        [Route("upfile")]//自定义路由
        [HttpPost]//请求方法为POST
        [AllowAnonymous]
        public UpFileViewModel UplodeFile([FromForm] IFormFile fromFile)
        {
            if (fromFile != null)
            {
                using (var sm = fromFile.OpenReadStream())
                {
                    string savePath = $"{Directory.GetCurrentDirectory()}/wwwroot/temp/{Id.LongId()}{fromFile.FileName.GetExtension()}";
                    sm.Save(savePath);
                }
                return new UpFileViewModel { code = 0, msg = "保存成功" };
            }
            return new UpFileViewModel { code = 1, msg = "保存失败" };
        }




        /// <summary>
        /// 上传多个文件,需要指定name值为files，如：form-data; name="files"; filename="8.png"
        /// </summary>
        /// <param name="fromFile"></param>
        /// <returns></returns>
        [Route("upfiles")]//自定义路由
        [HttpPost]//请求方法为POST
        [AllowAnonymous]
        public UpFileViewModel UplodeFiles([FromForm] IFormFileCollection files)
        {
            if (files != null)
            {
                files.ForEach(fromFile =>
                {
                    using (var sm = fromFile.OpenReadStream())
                    {
                        string savePath = $"{Directory.GetCurrentDirectory()}/wwwroot/temp/{Id.LongId()}{fromFile.FileName.GetExtension()}";
                        sm.Save(savePath);
                    }
                });
                return new UpFileViewModel { code = 0, msg = "保存成功" };
            }
            return new UpFileViewModel { code = 1, msg = "保存失败" };
        }


        /// <summary>
        /// 上传时不需要指定name参数:form-data; name=""; filename="222222222222.jpg"
        /// </summary>
        /// <returns></returns>
        [Route("upfiles2")]//自定义路由
        [HttpPost]//请求方法为POST
        [AllowAnonymous]
        public UpFileViewModel UplodeFiles2()
        {
            var files = this.Request.Form?.Files;
            if (files != null)
            {
                files.ForEach(fromFile =>
                {
                    using (var sm = fromFile.OpenReadStream())
                    {
                        string savePath = $"{Directory.GetCurrentDirectory()}/wwwroot/temp/{Id.LongId()}{fromFile.FileName.GetExtension()}";
                        sm.Save(savePath);
                    }
                });
                return new UpFileViewModel { code = 0, msg = "保存成功" };
            }
            return new UpFileViewModel { code = 1, msg = "保存失败" };
        }

    }


}
