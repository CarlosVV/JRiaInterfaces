using System;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Models;

namespace CES.CoreApi.Foundation.Tools
{
    public class MappingHelper : IMappingHelper
    {
        #region Core

        private readonly IAutoMapperProxy _mapper;

        public MappingHelper(IAutoMapperProxy mapper)
        {
            if (mapper == null) throw new ArgumentNullException("mapper");
            _mapper = mapper;
        }

        #endregion

        #region Public methods

        public TOut ConvertTo<TIn, TOut>(TIn model)
        {
            return _mapper.Map<TIn, TOut>(model);
        }

        public TOut ConvertToResponse<TIn, TOut>(TIn model)
        {
            var response = ConvertTo<TIn, TOut>(model);

            var baseResponse = response as BaseResponse;
            if (baseResponse != null)
                baseResponse.StatusCode = ResponseStatus.Success;

            return response;
        }

        #endregion
    }
}
