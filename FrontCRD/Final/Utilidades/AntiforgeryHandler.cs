namespace Final.Utilidades;


// Define uma classe chamada AntiforgeryHandler que herda de DelegatingHandler.
// Essa classe é usada para interceptar requisições HTTP e adicionar o token anti-CSRF.
public class AntiforgeryHandler : DelegatingHandler
{
    // Campo privado para acessar o contexto HTTP atual
    private readonly IHttpContextAccessor _accessor;

    // Construtor que recebe o IHttpContextAccessor via injeção de dependência
    public AntiforgeryHandler(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    // Sobrescreve o método SendAsync para interceptar requisições HTTP
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Obtém o contexto HTTP atual
        var context = _accessor.HttpContext;

        // Tenta recuperar o cookie chamado "XSRF-TOKEN"
        var token = context?.Request.Cookies["XSRF-TOKEN"];

        // Se o token estiver presente e não for vazio ou espaço em branco...
        if (!string.IsNullOrWhiteSpace(token))
        {
            // ... adiciona o token como um header da requisição HTTP
            request.Headers.Add("X-XSRF-TOKEN", token);
        }

        // Chama o método base para continuar o pipeline da requisição HTTP
        return await base.SendAsync(request, cancellationToken);
    }
}
