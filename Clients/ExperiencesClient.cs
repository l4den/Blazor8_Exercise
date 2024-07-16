using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CRUD.frontend.Models;

namespace CRUD.frontend.Clients
{
    public class ExperiencesClient(HttpClient httpClient)
    {
        public async Task<Experience[]> GetExperiencesAsync() 
            => await httpClient.GetFromJsonAsync<Experience[]>("api/experience") ?? [];    
    
        public async Task<HttpResponseMessage> AddExperienceAsync(Experience experience, int personId)
        {
            Random random = new Random();
            experience.Id = random.Next(1, 100000);
            HttpResponseMessage response = await httpClient.PostAsJsonAsync($"api/experience/{personId}", experience);
            
            return response;
        }

        public async Task<HttpResponseMessage> UpdateExperienceAsync(Experience updatedExperience)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync($"api/experience/{updatedExperience.Id}", updatedExperience);
            return response;
        }

        public async Task<Experience> GetExperienceAsync(int id)
        => await httpClient.GetFromJsonAsync<Experience>($"api/experience/{id}")
        ?? throw new Exception("Could not find experience");
        
        public async Task DeleteExperieceAsync(int id)
            => await httpClient.DeleteAsync($"api/experience/{id}");

        public async Task<Experience[]?> GetExperiencesByPersonIdAsync(int personId)
            => await httpClient.GetFromJsonAsync<Experience[]?>($"api/experience/personId/{personId}") ?? [];

    }
}