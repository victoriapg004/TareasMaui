from django.urls import path
from . import views

urlpatterns = [
    path("tareas/", views.lista_tareas),
]