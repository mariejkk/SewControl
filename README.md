## Descripción del proyecto

**SewControl** es un sistema de gestión para talleres de costura que permite administrar de forma centralizada tres pilares del negocio: **clientes**, **costureras** y **encargos** (pedidos de trabajo).

El sistema está diseñado bajo una arquitectura por capas (Domain, Application, Infrastructure, Persistence, API), separando claramente las responsabilidades del negocio, el acceso a datos y la exposición de servicios. La API expone endpoints REST protegidos con autenticación **JWT**, donde cada usuario debe registrarse e iniciar sesión para acceder a las operaciones del sistema.


### Funcionalidades principales

- **Gestión de clientes**: registro, edición, consulta y eliminación de clientes, con seguimiento de su historial de encargos.
- **Gestión de costureras**: registro de costureras, su especialidad, disponibilidad (activa/inactiva), y carga de trabajo actual.
- **Gestión de encargos**: creación de pedidos con tipo (confección, arreglo, bordado), fechas de entrega, precios y anticipos, además de seguimiento de estado (pendiente, en proceso, listo, entregado, cancelado).
- **Prendas y arreglos**: cada encargo puede desglosarse en prendas específicas (con tela, color, talla) y arreglos individuales (con su propio costo), permitiendo un control detallado del trabajo.
- **Dashboard**: vista general con estadísticas clave (total de encargos, pendientes, listos, clientes) y alertas de encargos atrasados.
- **Autenticación**: registro y login de usuarios con contraseñas cifradas (BCrypt) y tokens JWT para proteger el acceso a la información.
  

### Componentes del proyecto

- **Backend (API REST)**: desarrollado en ASP.NET Core, documentado con Swagger, y desplegado en producción.
- **Frontend (Blazor WebAssembly)**: interfaz visual con MudBlazor que consume la API, con su propio flujo de login/registro.
  

### Usabilidad y Accesibilidad

- **RNF8:** La interfaz del sistema debe ser intuitiva y fácil de usar para cualquier tipo de usuario, priorizando claridad visual y flujo natural de navegación.
- **RNF9:** La aplicación debe ser responsive, adaptándose correctamente a distintos dispositivos como móvil, tableta y escritorio.
  

## Cómo probar el proyecto

### 1. Probar la API en Swagger (producción, sin instalar nada)

Dale click [Despliegue SewControl](http://sewcontrol-api.somee.com/swagger/index.html) para ver el despliegue de la API en Swagger.

1. Expande `POST /api/Auth/register` → **"Try it out"** → completa:
```json
   {
     "nombre": "Tu Nombre",
     "email": "tucorreo@ejemplo.com",
     "password": "TuContraseña123!"
   }
```
2. **"Execute"** → copia el `token` de la respuesta (sin comillas)
3. Botón **"Authorize"** 🔒 arriba → pega el token (sin la palabra "Bearer") → **Authorize** → **Close**
4. Ahora prueba cualquier endpoint protegido, ej. `GET /api/Clientes` → **"Try it out"** → **"Execute"**

### 2. Probar el Frontend (Blazor) — solo disponible en local

> El frontend no está desplegado; solo la API está en producción. Para ver la interfaz visual necesitas correrla desde tu máquina.

1. Abre la solución en Visual Studio: `src/Backend/SewControlAPI.sln` (o donde tengas el `.sln` del frontend, según cómo esté organizado).
2. Click derecho en `SewControl.Web` (el proyecto Blazor) → **"Set as Startup Project"**.
3. Presiona **F5** (o el botón ▶ Start).
4. Se abre el navegador con el login.
5. Como es la primera vez, dale click a **"¿No tienes cuenta? Regístrate"** → crea tu cuenta.
6. Te devuelve al login → entra con esas credenciales.
7. Navega Inicio → Encargos / Clientes / Costureras y prueba crear, editar, eliminar.

### Importante: confirma contra qué API está apuntando el frontend

Abre `src/Fronted/SewControl.Web/Program.cs` y revisa esta línea:

```csharp
client.BaseAddress = new Uri("https://localhost:7119/");
```

- Si quieres que el frontend hable con tu **API local** → deja esto así y corre también la API localmente (`F5` en `SewControl.API`, o `dotnet run`), ambos proyectos corriendo a la vez.
- Si quieres que el frontend hable con tu **API ya desplegada en somee** (sin correr nada local del backend) → cámbiala a:

```csharp
  client.BaseAddress = new Uri("http://sewcontrol-api.somee.com/");
```
