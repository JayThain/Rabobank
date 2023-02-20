﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Rabobank.TechnicalTest.GCOB.Models.Entities;
using Rabobank.TechnicalTest.GCOB.Models.Repositories.Abstract;

namespace Rabobank.TechnicalTest.GCOB.Models.Repositories;

public interface ICountryRepository: IRepository<Country>
{
  Task<IEnumerable<Country>> GetAllAsync();
}