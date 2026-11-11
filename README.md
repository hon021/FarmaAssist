# FarmaAssist

Plataforma web multi-tenant (SaaS) para que farmacias organicen la atención de sus pacientes, sus documentos y su inventario. FarmaAssist apoya al personal y no reemplaza al profesional de salud: no diagnostica, no prescribe ni sustituye medicamentos.

**Piloto:** Droguería Villacolombia (Cali).

## Estado

Etapa 1 en curso (núcleo operativo del MVP). Ver la propuesta de MVP en [docs/FarmaAssist_Propuesta_MVP.md](docs/FarmaAssist_Propuesta_MVP.md) y el alcance en [docs/FarmaAssist_Alcance_MVP.md](docs/FarmaAssist_Alcance_MVP.md).

## Stack

- Backend: ASP.NET Core (.NET 10) Web API, ASP.NET Core Identity, Entity Framework Core.
- Base de datos: PostgreSQL con Npgsql.
- Frontend: Angular (pendiente de la Etapa 1).
- IA (Etapa 4): Qwen mediante Ollama, en infraestructura controlada.
- Despliegue piloto: local.

## Estructura

```
docs/                 Alcance, propuesta y mockup
src/FarmaAssist.Api/  API, dominio, acceso a datos y multi-tenant
```

## Requisitos

- .NET SDK 10
- PostgreSQL 16 o superior

## Configuración local

1. Crear la base de datos `farmaassist` en PostgreSQL.
2. Configurar la cadena de conexión con user secrets (no guardar contraseñas en `appsettings.json`):

   ```powershell
   cd src/FarmaAssist.Api
   dotnet user-secrets set "ConnectionStrings:Default" "Host=localhost;Port=5432;Database=farmaassist;Username=farmaassist;Password=<CONTRASEÑA>"
   ```

3. Aplicar las migraciones incluidas en el repositorio:

   ```powershell
   dotnet tool install --global dotnet-ef
   dotnet ef database update
   ```

4. Ejecutar la API:

   ```powershell
   dotnet run
   ```

## Multi-tenant

- Cada farmacia es un `Tenant`. Pacientes, atenciones e inventario llevan `TenantId`.
- Un filtro global de EF Core limita las consultas al tenant del usuario autenticado.
- `TenantId` se asigna automáticamente al guardar entidades nuevas.
- Las pruebas de aislamiento entre farmacias son obligatorias antes de cargar datos reales.

## Endpoints iniciales

- `POST /auth/register`, `POST /auth/login`: Identity API (tokens bearer).
- `GET /api/patients`: lista pacientes del tenant (máximo 200).
- `POST /api/patients`: crea paciente; rechaza duplicados por tipo y número de documento.

## Privacidad

El sistema maneja datos de salud. No usar datos reales hasta cerrar la revisión de privacidad, el contrato de tratamiento de datos y la validación del responsable de la farmacia.

## Pendiente

- Frontend Angular.
- Alta y configuración de farmacia (tenant) y administración de usuarios.
- Pruebas automatizadas de aislamiento entre tenants.
- Importación de inventario desde Excel.
