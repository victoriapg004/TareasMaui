# Mis Tareas — App móvil con .NET MAUI

Aplicación de gestión de tareas desarrollada con **.NET MAUI** (C# + XAML). Permite registrar tareas con validaciones, listarlas con su prioridad y **consumir un endpoint REST de una API Django** con `HttpClient` y `GetFromJsonAsync`.

## Funcionalidades

- Lista de tareas con etiqueta de color según prioridad (Alta, Media, Baja).
- Formulario de nueva tarea con validación (campos obligatorios y mínimo de caracteres).
- Carga de tareas desde la API Django, con indicador de carga y banner de estado.
- Botón **Actualizar** y *pull-to-refresh* para volver a consultar la API.
- Si la API está apagada, tarda o responde mal, la app no se cierra: muestra un mensaje y usa datos de ejemplo.

## Requisitos

| Componente | Versión / nota |
|---|---|
| .NET SDK | 10.0 (fijado en `global.json`) |
| Workload de MAUI | `dotnet workload install maui` |
| JDK | **21** (con el JDK 25 la compilación para Android falla con el error XA0030) |
| Android SDK + emulador | Plataforma API 36 instalada (se gestiona desde Android Studio) |
| Python 3.10+ y Django | Solo para levantar la API |

## Instalación

```bash
git clone <URL-DEL-REPOSITORIO>
cd TareasMaui
dotnet restore
```

Backend (en la carpeta `backend`):

```bash
cd backend
py -m venv venv
venv\Scripts\activate
pip install django
```

## Ejecución

### 1. Levantar la API Django

```bash
cd backend
venv\Scripts\activate
python manage.py runserver 0.0.0.0:8000
```

Comprobar en el navegador: `http://localhost:8000/api/tareas/` debe mostrar un JSON.

### 2. Ejecutar la app

```bash
# Windows
dotnet run -f net10.0-windows10.0.19041.0

# Emulador Android (debe estar iniciado)
dotnet build -t:Run -f net10.0-android "-p:JavaSdkDirectory=<RUTA-JDK-21>" "-p:AndroidSdkDirectory=<RUTA-ANDROID-SDK>"
```

Cambiar `<RUTA-JDK-21>` y `<RUTA-ANDROID-SDK>` por las rutas de cada equipo.

## Configuración de la API

Todo está en `Services/ApiConfig.cs`:

| Dato | Valor |
|---|---|
| URL en emulador Android | `http://10.0.2.2:8000/` (en el emulador `localhost` es el propio emulador) |
| URL en Windows | `http://localhost:8000/` |
| Endpoint | `api/tareas/` |
| Timeout | 8 segundos |

Para un celular físico, usar la IP local de la PC en lugar de `10.0.2.2`.

### Formato JSON esperado

```json
[
  { "id": 1, "titulo": "Tarea desde Django", "descripcion": "Estos datos vienen de la API REST", "prioridad": "Alta" }
]
```

### Tráfico HTTP en Android

Android bloquea HTTP sin cifrar por defecto. Se habilitó solo para `10.0.2.2` y `localhost` en `Platforms/Android/Resources/xml/network_security_config.xml`. En producción debe usarse HTTPS.

## Datos de prueba

- `Resources/Raw/tareas_ejemplo.json`: datos ficticios que la app carga si la API no responde (copia en `docs/`).
- Los datos de la API están en `backend/tareas/views.py`.
- No hay credenciales, tokens ni claves en el repositorio. `.env` y `*.keystore` están en `.gitignore`.

## Estructura del proyecto

```
TareasMaui/
├── backend/           API Django de ejemplo (endpoint api/tareas/)
├── Models/            Tarea.cs
├── Services/          ApiConfig.cs, TareasApiService.cs (HttpClient)
├── Data/              DatosTareas.cs (lista en memoria)
├── MainPage.xaml(.cs)                  Lista de tareas y estado de la API
├── TareaFormPage.xaml(.cs)             Formulario con validaciones
├── Platforms/         Código específico por plataforma
├── Resources/         Estilos, imágenes, Raw/tareas_ejemplo.json
└── docs/              Pruebas, capturas e informe
```

## Limitaciones conocidas

- Las tareas creadas en la app solo viven en memoria: se pierden al cerrar la aplicación.
- La app solo lee de la API (GET). Las tareas nuevas no se envían al servidor (POST).
- No hay edición, eliminación ni marcar tareas como completadas.
- No hay autenticación contra la API.
- La API de ejemplo devuelve datos fijos, sin base de datos.
- Probado en Windows y en emulador Android (Pixel 8, API 36). [CONFIRMAR] iOS y macOS no se probaron: requieren una Mac.

## Pruebas

Ver [`docs/PRUEBAS.md`](docs/PRUEBAS.md) y las capturas en `docs/capturas/`.

## Autores

Proyecto académico — Victoria, Brayan Mena Sancho y Aracelly.