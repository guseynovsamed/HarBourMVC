using System;
using System.Collections.Generic;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface ISliderVideoService
	{
        Task <IEnumerable <SliderVideo>> GetAll();
        Task<SliderVideo> GetById(int id);
        Task Create(SliderVideo slider);
        Task Edit(int id, SliderVideo slider);
        Task Delete(SliderVideo slider);
    }
}

