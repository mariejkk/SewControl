using Microsoft.JSInterop;
using SewControl.Web.Models;
using System.Net.Http.Json;

namespace SewControl.Web.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;

    public AuthService(HttpClient http, IJSRuntime js)
    {
        _http = http;
        _js = js;
    }

    public async Task<(bool ok, string? message)> LoginAsync(LoginDto dto)
    {
        var res = await _http.PostAsJsonAsync("api/auth/login", dto);
        var r = await res.Content.ReadFromJsonAsync<ApiResponse<LoginResponseDto>>();

        if (r?.Success == true && r.Data != null)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", "token", r.Data.Token);
            await _js.InvokeVoidAsync("localStorage.setItem", "usuario", r.Data.Nombre);
            return (true, r.Message);
        }

        return (false, r?.Message ?? "Error al iniciar sesión.");
    }

    public async Task LogoutAsync()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", "token");
        await _js.InvokeVoidAsync("localStorage.removeItem", "usuario");
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _js.InvokeAsync<string?>("localStorage.getItem", "token");
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await GetTokenAsync();
        return !string.IsNullOrEmpty(token);
    }
}

public class AuthHttpHandler : DelegatingHandler
{
    private readonly IJSRuntime _js;

    public AuthHttpHandler(IJSRuntime js)
    {
        _js = js;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _js.InvokeAsync<string?>("localStorage.getItem", "token");

        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}

public class ClienteService
{
    private readonly HttpClient _http;
    public ClienteService(HttpClient http) => _http = http;

    public async Task<List<ClienteDto>> GetAllAsync()
    {
        var r = await _http.GetFromJsonAsync<ApiResponse<List<ClienteDto>>>("api/clientes");
        return r?.Data ?? new();
    }

    public async Task<ClienteDto?> GetByIdAsync(int id)
    {
        var r = await _http.GetFromJsonAsync<ApiResponse<ClienteDto>>($"api/clientes/{id}");
        return r?.Data;
    }

    public async Task<(bool ok, string? message, List<string>? errors)> CreateAsync(CreateClienteDto dto)
    {
        var res = await _http.PostAsJsonAsync("api/clientes", dto);
        var r = await res.Content.ReadFromJsonAsync<ApiResponse<ClienteDto>>();
        return (r?.Success ?? false, r?.Message, r?.Errors);
    }

    public async Task<(bool ok, string? message)> UpdateAsync(int id, CreateClienteDto dto)
    {
        var res = await _http.PutAsJsonAsync($"api/clientes/{id}", dto);
        var r = await res.Content.ReadFromJsonAsync<ApiResponse<ClienteDto>>();
        return (r?.Success ?? false, r?.Message);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var res = await _http.DeleteAsync($"api/clientes/{id}");
        var r = await res.Content.ReadFromJsonAsync<ApiResponse<bool>>();
        return r?.Success ?? false;
    }
}

public class CostureraService
{
    private readonly HttpClient _http;
    public CostureraService(HttpClient http) => _http = http;

    public async Task<List<CostureraDto>> GetAllAsync()
    {
        var r = await _http.GetFromJsonAsync<ApiResponse<List<CostureraDto>>>("api/costureras");
        return r?.Data ?? new();
    }

    public async Task<(bool ok, string? message, List<string>? errors)> CreateAsync(CreateCostureraDto dto)
    {
        var res = await _http.PostAsJsonAsync("api/costureras", dto);
        var r = await res.Content.ReadFromJsonAsync<ApiResponse<CostureraDto>>();
        return (r?.Success ?? false, r?.Message, r?.Errors);
    }

    public async Task<bool> ToggleActivaAsync(int id)
    {
        var res = await _http.PatchAsync($"api/costureras/{id}/toggle-activa", null);
        var r = await res.Content.ReadFromJsonAsync<ApiResponse<bool>>();
        return r?.Success ?? false;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var res = await _http.DeleteAsync($"api/costureras/{id}");
        var r = await res.Content.ReadFromJsonAsync<ApiResponse<bool>>();
        return r?.Success ?? false;
    }

    public async Task<(bool ok, string? message)> UpdateAsync(int id, CreateCostureraDto dto)
    {
        var res = await _http.PutAsJsonAsync($"api/costureras/{id}", dto);
        var r = await res.Content.ReadFromJsonAsync<ApiResponse<CostureraDto>>();
        return (r?.Success ?? false, r?.Message);
    }

}

public class EncargoService
{
    private readonly HttpClient _http;
    public EncargoService(HttpClient http) => _http = http;

    public async Task<List<EncargoDto>> GetAllAsync()
    {
        var r = await _http.GetFromJsonAsync<ApiResponse<List<EncargoDto>>>("api/encargos");
        return r?.Data ?? new();
    }

    public async Task<EncargoDto?> GetByIdAsync(int id)
    {
        var r = await _http.GetFromJsonAsync<ApiResponse<EncargoDto>>($"api/encargos/{id}");
        return r?.Data;
    }

    public async Task<List<EncargoDto>> GetByClienteAsync(int clienteId)
    {
        var r = await _http.GetFromJsonAsync<ApiResponse<List<EncargoDto>>>($"api/encargos/cliente/{clienteId}");
        return r?.Data ?? new();
    }

    public async Task<List<EncargoDto>> GetByEstadoAsync(EstadoEncargo estado)
    {
        var r = await _http.GetFromJsonAsync<ApiResponse<List<EncargoDto>>>($"api/encargos/estado/{(int)estado}");
        return r?.Data ?? new();
    }

    public async Task<(bool ok, string? message, List<string>? errors)> CreateAsync(CreateEncargoDto dto)
    {
        var res = await _http.PostAsJsonAsync("api/encargos", dto);
        var r = await res.Content.ReadFromJsonAsync<ApiResponse<EncargoDto>>();
        return (r?.Success ?? false, r?.Message, r?.Errors);
    }

    public async Task<(bool ok, string? message)> UpdateAsync(int id, UpdateEncargoDto dto)
    {
        var res = await _http.PutAsJsonAsync($"api/encargos/{id}", dto);
        var r = await res.Content.ReadFromJsonAsync<ApiResponse<EncargoDto>>();
        return (r?.Success ?? false, r?.Message);
    }

    public async Task<(bool ok, string? message)> UpdateEncargoAsync(int id, CreateEncargoDto dto)
    {
        var res = await _http.PutAsJsonAsync($"api/encargos/editar/{id}", dto);
        var r = await res.Content.ReadFromJsonAsync<ApiResponse<EncargoDto>>();
        return (r?.Success ?? false, r?.Message);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var res = await _http.DeleteAsync($"api/encargos/{id}");
        var r = await res.Content.ReadFromJsonAsync<ApiResponse<bool>>();
        return r?.Success ?? false;
    }

    public async Task<(bool ok, string? message, List<string>? errors)> AddPrendaAsync(CreatePrendaDto dto)
    {
        var res = await _http.PostAsJsonAsync("api/encargos/prendas", dto);
        var r = await res.Content.ReadFromJsonAsync<ApiResponse<PrendaDto>>();
        return (r?.Success ?? false, r?.Message, r?.Errors);
    }

    public async Task<bool> DeletePrendaAsync(int id)
    {
        var res = await _http.DeleteAsync($"api/encargos/prendas/{id}");
        var r = await res.Content.ReadFromJsonAsync<ApiResponse<bool>>();
        return r?.Success ?? false;
    }

    public async Task<(bool ok, string? message, List<string>? errors)> AddArregloAsync(CreateArregloDto dto)
    {
        var res = await _http.PostAsJsonAsync("api/encargos/arreglos", dto);
        var r = await res.Content.ReadFromJsonAsync<ApiResponse<ArregloDto>>();
        return (r?.Success ?? false, r?.Message, r?.Errors);
    }

    public async Task<bool> DeleteArregloAsync(int id)
    {
        var res = await _http.DeleteAsync($"api/encargos/arreglos/{id}");
        var r = await res.Content.ReadFromJsonAsync<ApiResponse<bool>>();
        return r?.Success ?? false;
    }
}
