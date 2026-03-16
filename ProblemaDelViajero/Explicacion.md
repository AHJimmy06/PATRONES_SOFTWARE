# Explicación: Problema del Viajero (Branch and Bound)

## Descripción del problema
El Problema del Viajero (TSP) busca encontrar la ruta más corta que visite un conjunto de ciudades exactamente una vez y regrese a la ciudad de origen. En este caso, se ha implementado una versión simplificada con 6 ciudades de Ecuador, utilizando una matriz de distancias para calcular el costo total de la ruta.

## Modelado del estado
El estado del problema se representó mediante la clase `TspState`, la cual contiene:
- **Estructura de datos:** Una lista de enteros (`List<int>`) que almacena los índices de las ciudades en el orden visitado.
- **Datos específicos:** La distancia acumulada hasta el momento (`CurrentDistance`) y la ciudad actual (`CurrentCity`).
- **Generación de nuevos estados:** Para generar un nuevo estado, se clona el estado actual y se añade una ciudad que aún no ha sido visitada, actualizando la distancia recorrida según la matriz de costos.

## Representación del nodo
Siguiendo los patrones de búsqueda, se utilizó la clase `SearchNode` que actúa como un nodo en el árbol de expansión:
- **Estado:** Una instancia de `TspState`.
- **Nodo Padre:** Referencia al nodo desde el cual se generó (permite reconstruir el camino).
- **Costo (g):** La distancia real recorrida desde el origen hasta el estado actual.
- **Profundidad:** El número de ciudades visitadas en ese nodo.
- **Límite Inferior (f):** Una estimación optimista del costo total (distancia actual + estimación de distancias mínimas para las ciudades restantes).

## Algoritmo de búsqueda utilizado y Explicación
Se utilizó el algoritmo **Branch and Bound (Ramificación y Poda)** con una estrategia de búsqueda "Best-First":
1. **Inicialización:** Se crea un nodo raíz con la ciudad inicial y se coloca en una cola de prioridad basada en el límite inferior.
2. **Expansión:** En cada paso, se extrae el nodo con el menor límite inferior (el más prometedor).
3. **Poda (Bounding):** Si el límite inferior de un nodo es mayor o igual a la mejor distancia completa encontrada hasta el momento, el nodo se descarta (poda), evitando explorar ramas innecesarias.
4. **Finalización:** El proceso continúa hasta que no queden nodos por explorar en la cola de prioridad. Al llegar a una solución completa (todas las ciudades visitadas), se suma el regreso a la ciudad de origen y se actualiza la mejor solución.

## Resultados obtenidos
```text
=== Problema del Viajero (Branch and Bound) ===
Ciudades a visitar: Quito, Guayaquil, Cuenca, Ambato, Manta, Loja
-----------------------------------------------

RESULTADO ENCONTRADO:
Ruta óptima: Quito -> Ambato -> Manta -> Guayaquil -> Loja -> Cuenca -> Quito
Distancia total: 1220 km
```
