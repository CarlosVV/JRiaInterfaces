﻿using AutoMapper;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Api.ViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace CES.CoreApi.GeoLocation.Api
{
	[RoutePrefix("geolocation")]
	public class MapController : ApiController
	{
		private readonly IMapper mapper;
		private readonly IMapServiceRequestProcessor mapServiceRequestProcessor;
		public MapController(IMapper mapper, IMapServiceRequestProcessor mapServiceRequestProcessor)
		{
			this.mapper = mapper;
			this.mapServiceRequestProcessor = mapServiceRequestProcessor;
		}
		[HttpPost]
		[Route("map")]
		public virtual GetMapResponse GetMap(GetMapRequest request)
		{
			//_validator.Validate(request);

			var responseModel = mapServiceRequestProcessor.GetMap(
				request.Country,
				 mapper.Map<Location, LocationModel>(request.Center),
				mapper.Map<MapSize, MapSizeModel>(request.MapSize),
				mapper.Map<MapOutputParameters, MapOutputParametersModel>(request.MapOutputParameters),
				mapper.Map<ICollection<PushPin>, ICollection<PushPinModel>>(request.PushPins));

			return mapper.Map<GetMapResponseModel, GetMapResponse>(responseModel);
		}

	}
}