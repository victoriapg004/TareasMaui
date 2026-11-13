from django.http import JsonResponse

TAREAS = [
    {"id": 1, "titulo": "Tarea desde Django", "descripcion": "Estos datos vienen de la API REST", "prioridad": "Alta"},
    {"id": 2, "titulo": "Probar la conexión", "descripcion": "Verificar que MAUI consume el endpoint", "prioridad": "Media"},
    {"id": 3, "titulo": "Documentar en GitHub", "descripcion": "Subir capturas y README al repositorio", "prioridad": "Baja"},
]

def lista_tareas(request):
    return JsonResponse(TAREAS, safe=False)