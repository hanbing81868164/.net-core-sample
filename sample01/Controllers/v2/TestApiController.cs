using Microsoft.AspNetCore.Mvc;
using sample01.Models;
using System;

namespace sample01.Controllers.v2
{
    [Produces("application/json")]//Controller默认输出json格式
    //[Route("api/v1/TestApi")]
    //[Route("api/v{version:ApiVersion}/[controller]")]
    [Route("api/v2/testapi")]
    public class TestApiController : Controller
    {

        /// <summary>
        /// 返回用户信息s
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getuser")]
        //[Produces("application/xml")]//输出xml格式
        public UserViewMmodel Index()
        {
            return new UserViewMmodel
            {
                address = "上海市浦东区世纪大道200号",
                age = 23,
                creation_time = DateTime.Now,
                qq = "123456789",
                user_name = "hanbing",
                version = "v2.0"
            };
        }
    }
}
