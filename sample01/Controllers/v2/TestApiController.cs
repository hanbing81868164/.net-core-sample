using Microsoft.AspNetCore.Mvc;
using sample01.Models;
using System;
using System.Threading.Tasks;

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
        public Task<ViewModelBase<UserViewMmodel>> Index()
        {
            return Task.Run(() =>
            {
                ViewModelBase<UserViewMmodel> res = new ViewModelBase<UserViewMmodel>();
                //业务逻辑代码....
                res = new ViewModelBase<UserViewMmodel>
                {
                    code = 0,
                    msg = "成功返回用户信息",
                    data = new UserViewMmodel
                    {
                        address = "上海市浦东区世纪大道200号",
                        age = 23,
                        creation_time = DateTime.Now,
                        version = "v2.0"
                    }
                };

                return res;
            });
        }
    }
}
