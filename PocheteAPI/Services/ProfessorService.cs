using PocheteAPI.DTO;
using System.Net.Http.Json;

namespace PocheteAPI.Services {
    public class ProfessorService {
        private readonly HttpClient _http;

        public ProfessorService(HttpClient http) {
            _http = http;
        }

        public async Task<List<ProfessoresDTO>> ListarAsync()
        {
            return await _http.GetFromJsonAsync<List<ProfessoresDTO>>("api/Professores");
        }

        public async Task<ProfessoresDTO?> BuscarPorIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<ProfessoresDTO>($"api/Professores/{id}");
        }

        public async Task<(bool sucesso, string mensagem)> CadastrarAsync(ProfessoresDTO professor)
        {
            var response = await _http.PostAsJsonAsync("api/Professores", professor);
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Response Status: {response.StatusCode}, Content: {content}");  // Log para verificar o status

            return response.IsSuccessStatusCode
                ? (true, "Professor cadastrado com sucesso!")
                : (false, $"Erro ao cadastrar o professor: {content}");
        }


        public async Task<bool> AtualizarAsync(int id, ProfessoresDTO professor)
        {
            var response = await _http.PutAsJsonAsync($"api/Professores/{id}", professor); // Corrigido para plural
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/Professores/{id}"); // Corrigido para plural
            return response.IsSuccessStatusCode;
        }
    }
}
