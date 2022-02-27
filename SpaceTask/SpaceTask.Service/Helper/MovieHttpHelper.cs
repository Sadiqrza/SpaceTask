using Microsoft.Extensions.Options;
using SpaceTask.Data.DataModels;
using SpaceTask.Data.Dtos;
using SpaceTask.Service.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SpaceTask.Service.Helper
{
    public class MovieHttpHelper : IMovieHttpHelper
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ApiOptions _apiOptions;

        public MovieHttpHelper(IOptions<ApiOptions> options, IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            _apiOptions = options.Value;
        }

        public async Task<List<SearchResult>> GetMovieByName(string movieName)
        {
            SearchData searchData;
            var client = _httpClient.CreateClient();
            try
            {
                searchData = await client.GetFromJsonAsync<SearchData>(
                    $"{_apiOptions.SearchApi}/{_apiOptions.ApiKey}/{movieName}");
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
                throw;
            }
            return searchData?.Results;
        }

        public async Task<ImDbMovieGenre> GetMovieGenre(string movieId)
        {
            ImDbMovieGenre movieGenre;

            var client = _httpClient.CreateClient();

            try
            {
                movieGenre = await client.GetFromJsonAsync<ImDbMovieGenre>($"{_apiOptions.TitleApi}/{_apiOptions.ApiKey}/{movieId}");
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
                throw;
            }

            return movieGenre;
        }

        public async Task<ImDbMovieRating> GetImDbRatingByMovieId(string movieId)
        {
            ImDbMovieRating movieRating;
            var client = _httpClient.CreateClient();
            try
            {
                movieRating = await client.GetFromJsonAsync<ImDbMovieRating>(
                    $"{_apiOptions.TitleApi}/{_apiOptions.ApiKey}/{movieId}");
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
                throw;
            }
            return movieRating;
        }

        public async Task<PosterDataDto> GetPosterByMovieId(string movieId)
        {
            PosterDataDto posterData;
            var client = _httpClient.CreateClient();
            try
            {
                posterData = await client.GetFromJsonAsync<PosterDataDto>(
                    $"{_apiOptions.PosterApi}/{_apiOptions.ApiKey}/{movieId}");
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
                throw;
            }
            return posterData;
        }

        public async Task<WikipediaDataDto> GetDescriptionByMovieId(string movieId)
        {
            WikipediaDataDto wikipediaData;
            var client = _httpClient.CreateClient();
            try
            {
                wikipediaData = await client.GetFromJsonAsync<WikipediaDataDto>(
                    $"{_apiOptions.PosterApi}/{_apiOptions.ApiKey}/{movieId}");
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
                throw;
            }
            return wikipediaData;
        }
    }
}
