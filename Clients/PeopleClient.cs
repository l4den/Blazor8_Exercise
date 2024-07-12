using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using CRUD.frontend.Models;
using Microsoft.JSInterop;

namespace CRUD.frontend.Clients
{
    public class PeopleClient(HttpClient httpClient)
    {
        public async Task<(Person[]?, int)> GetPeopleAsync(string query = "")
        { 
            using var responseStream = await httpClient.GetStreamAsync("api/person" + query);
            var document = await JsonDocument.ParseAsync(responseStream);

            var peopleJson = document.RootElement.GetProperty("people").ToString();
            var people = JsonSerializer.Deserialize<Person[]>(peopleJson, new JsonSerializerOptions{PropertyNameCaseInsensitive = true });

            var totalRows = document.RootElement.GetProperty("totalRows").GetInt32();

            return (people, totalRows);
        }
        public async Task AddPersonAsync(Person person)
        {
            Random random = new Random();
            person.Id = random.Next(1, 100000);
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/person", person);

            response.EnsureSuccessStatusCode();
        }

        public async Task<Person> GetPersonAsync(int id) 
            => await httpClient.GetFromJsonAsync<Person>($"api/person/{id}")
            ?? throw new Exception("Could not find person");
        
        public async Task UpdatePersonAsync(Person updatedPerson)
            => await httpClient.PutAsJsonAsync($"api/person/{updatedPerson.Id}", updatedPerson);

        public async Task DeletePersonAsync(int id)
            => await httpClient.DeleteAsync($"api/person/{id}");

        public async Task<bool> EmbgExistsAsync(string embg)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"api/person/embg/{embg}");
            string json = await response.Content.ReadAsStringAsync();
            bool exists = JsonSerializer.Deserialize<bool>(json);
            return exists;
        }
        // DOWNLOADS
        public async Task<byte[]> GetCsvFileBytesAsync(string query)
        {
            HttpResponseMessage response = await httpClient.GetAsync("api/person/download-csv"+query);
            response.EnsureSuccessStatusCode();

            byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();

            return fileBytes;
        }

        public async Task<byte[]> GetPdfFileBytesAsync(string query)
        {
            HttpResponseMessage response = await httpClient.GetAsync("api/person/download-pdf"+query);
            response.EnsureSuccessStatusCode();

            byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();
            return fileBytes;
        }

        public async Task<byte[]> GetXlxsFileBytesAsync(string query)
        {
            HttpResponseMessage response = await httpClient.GetAsync("api/person/download-xlxs"+query);
            response.EnsureSuccessStatusCode();

            byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();
            return fileBytes;
        }
    }
}