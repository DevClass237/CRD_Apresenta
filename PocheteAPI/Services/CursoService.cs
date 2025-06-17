using PocheteAPI.DTO;
using System.Net.Http.Json;

namespace PocheteAPI.Services
{
    public class CursoService
    {
        private readonly HttpClient _http;

        public CursoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CursosDTO>> ListarAsync()
        {
            return await _http.GetFromJsonAsync<List<CursosDTO>>("api/cursos");
        }

        public async Task<CursosDTO?> BuscarPorIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<CursosDTO>($"api/Curso/{id}");
        }

        public async Task<(bool sucesso, string mensagem)> CadastrarAsync(CursosDTO curso)
        {
            var response = await _http.PostAsJsonAsync("api/Curso", curso);
            var content = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode
                ? (true, "Curso cadastrado com sucesso!")
                : (false, $"Erro ao cadastrar o curso: {content}");
        }

        public async Task<bool> AtualizarAsync(int id, CursosDTO curso)
        {
            var response = await _http.PutAsJsonAsync($"api/Curso/{id}", curso);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/Curso/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
