# Explicación: Búsqueda de Ruta en un Mapa (Dijkstra)

## Descripción del problema
Este problema consiste en encontrar la ruta más corta entre dos ciudades dadas en un mapa, donde las ciudades están conectadas por carreteras con distancias específicas. Se ha modelado un grafo donde los nodos son ciudades y las aristas son las carreteras con sus respectivos kilometrajes.

## Modelado del estado
El estado del problema se representó mediante la clase `MapState`:
- **Estructura de datos:** Un objeto simple que contiene el nombre de la ciudad actual (`string CityName`).
- **Datos específicos:** Únicamente la ubicación actual del agente en el grafo.
- **Generación de nuevos estados:** Los nuevos estados se generan consultando el diccionario del grafo (`_graph`), el cual devuelve una lista de ciudades adyacentes a la ciudad actual junto con la distancia de la carretera que las une.

## Representación del nodo
Se utilizó la clase `SearchNode` para representar cada paso en la exploración del mapa:
- **Estado:** Una instancia de `MapState` que indica en qué ciudad se encuentra el nodo.
- **Nodo Padre:** Referencia al nodo anterior en la ruta para permitir la reconstrucción del camino final.
- **Costo (G):** La distancia total acumulada desde la ciudad de origen hasta la ciudad de este nodo.
- **Profundidad:** El número de conexiones (carreteras) recorridas hasta el momento.
- **Acción aplicada:** En este modelo de grafos, la acción es el viaje hacia la ciudad vecina.

## Algoritmo de búsqueda utilizado y Explicación
Se implementó el algoritmo de **Dijkstra** (Búsqueda de Costo Uniforme):
1. **Inicialización:** Se coloca el nodo de la ciudad de origen en una lista abierta (`openList`) con un costo acumulado (G) de 0.
2. **Selección:** Mientras la lista abierta no esté vacía, se selecciona el nodo con el menor valor de G.
3. **Validación de Objetivo:** Si el nodo seleccionado contiene la ciudad de destino, se ha encontrado la ruta óptima y se procede a reconstruir el camino usando las referencias a los nodos padres.
4. **Expansión:** Si no es el destino, se marca la ciudad como visitada (añadiéndola a `closedList`) y se exploran todos sus vecinos.
5. **Actualización de Costos:** Para cada vecino, se calcula el nuevo costo acumulado. Si el vecino no ha sido visitado o se ha encontrado un camino más corto hacia él, se añade o actualiza en la lista abierta.

## Resultados obtenidos
```text
=== Búsqueda de Ruta más Corta (Dijkstra) ===
Origen: Quito
Destino: Cuenca

RUTA ENCONTRADA:
Quito -> Ambato -> Riobamba -> Cuenca
Distancia total: 380 km
```
