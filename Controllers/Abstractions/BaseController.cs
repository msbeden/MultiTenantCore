using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Multiple.Controllers.Abstractions
{
    public class BaseController : Controller
    {
        protected IMapper _mapper;
        protected ILogger _logger;
        public BaseController(IMapper mapper, ILoggerFactory loggerFactory)
        {
            this._mapper = mapper;
            this._logger = loggerFactory.CreateLogger(this.GetType());
        }
    }
}