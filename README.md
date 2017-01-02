# Nic.Galaxy
- Sistema informático para la predicción del clima de la galaxia que conforman las tres civilizaciones: Vulcanos, Ferengis y Betasoides.

<b>Autor: </b>
- Rodriguez Nicolas

<b>Interpretaciones y aclaraciones de la premisas y Bonus:</b>

- Se toma como el sistema galáctivo empieza el día 0, en donde los 3 plantes y el sol, estan alineados y cuyo valor del coseno es 1 (cos(0) = 1).

- Los posibles estados climáticos son:
* Lluvia: Cuando los 3 Planetas forman un triángulo y el Sol se encuentra dentro del perímetro. Y su pico de intensidad, cuando el perímetro está en su máximo valor.
* Sequía: Cuando los 3 planetas y el sol, están alineados entre sí.
* Óptima: Cuando los 3 planetas están alineados entre sí, pero no el Sol, se encuentra óiptima de presión y temperatura.
* Normal: Cuando no se cumple con ninguna de los 3 escenarios arriba mencionados.
  
- En la premisas, habla de poder predecir en los próximos 10 años. En este caso, cada planeta dura distintos días para cumplir un año, ya que un año, significa cuanto tarda el planeta en dar la vuelta completa al Sol. 
  Para calcular cuantos días tiene un año, y sabiendo que un año, sería dar la vuelta completa al sol y volver al punto inicial, sabiendo que nos basamos que el valor del coseno es 1, y el coseno de 360 es una vuelta entera, se divide 360 por la velocidad del planeta.
  Ejemplo: El Planeta Vulcano, tiene una velocidad de 5 grados/día, entonces, para dar una vuelta al sol, sería 360/5 = 72 días. Multiplicados por 10 años, se saca las estadísticas de los primeros 720 días.

- El proyecto se encaró para que el sistema pudiera estar preparado para más de una galaxia (haciendo que sea flexible para agregar otra galaxia y programando su módulo para las predicciones, según su regla).

- Se utilizá el almacenamiento de una base relacional para guardar la galaxia, con sus planetas y sus predicciones de los primeros 10 años del planeta que más tarda en completar todo el ciclo.

- Se crearon 3 tipos de servicio:
* Inicializador: Para que el sistema calcule y almacene las predicciones de los tiempos.
* Predición: Recibiendo el nombre del planeta y el tiempo, el servicio retorna cuantos días/períodos va a aparecer, y dato adiccional, en caso de lluvia, que día es de mayor intensidad.
* Clima: Recibiendo el día de la "galaxia", que clima se va a encontrar para los 3 plantes.
> Nota: Si se quiere utilizar la Predicción y el clima, sin haberse ejecutado previamente el Inicializador, se producirá una excepción de tipo Nic.Galaxy.Exception.ValidException pidiendo que se ejecute el Inicializador, asi tiene toda la información necesaria.

- Se creó una api y un ejecutable para poder utilizar los servicios, obteniendo los mismos resultados.

---

<b>Características del proyecto:</b>

- El proyecto fue encarado y desarrollado con las siguientes tecnología y packages: 
* Framework .Net v4.6.1
* C#
* Nuget
* NHibernate
* FluentNHibernate
* Spring
* MySql.Data 
* NDesk.Options 
* Strathweb.CacheOutput.WebApi2
* Bases de datos MySql

- Las fuentes se encuentra en Github publicamente para poder clonarlo y/o bajarlo, analizarlo y compilarlo: https://github.com/rodrigueznicolas1975/Nic.Galaxy

- La api y base de datos, fue montada sobre un servidor gratuito que es totalmente integrada con .Net, llamado appHarbor: https://appharbor.com/

---

<b>Url de api:</b>

* [http://nicgalaxy.apphb.com/iniciar/{true|false}]
> El Job para inicializar las predicciones de la galaxia, true: forzando rehacer los cálculos, false: verifica si tiene todo calculado, sino los vuelve a calcular. 
> Nota: Este REST, debería haber sido un POST, pero para facilitar la prueba, se lo configuró con GET, así se podía facilitar la ejecución del RES 

* [http://nicgalaxy.apphb.com/planeta/{planeta}/estadistica/{clima}]
> Devuelve la cantidad de días que tuvo en un planeta determinado <b>['vulcano', 'ferengis', 'betasoides']</b> y el tipo de clima <b>['normal', 'sequia', 'lluvia', 'optimo']</b>

* [http://nicgalaxy.apphb.com/estadistica/{clima}]
> Devuelve la cantidad de días que tuvo en el planeta vulcano y el tipo de clima <b>['normal', 'sequia', 'lluvia', 'optimo']</b>

* [http://nicgalaxy.apphb.com/clima?dia={numerodia}]
> Devuelve el clima según el día de la galaxia.

---

<b>Ejecución mediante ejecutable:</b>

- Como todo esta centrado en un servicio, se creó tambien en el proyecto un ejecutable, en la cual, se puede ejecutar los mismos servicios que la api.
> Nota: En Github, se subieron el paquete compilado que se encuentra en el directorio "processes", lo que si, en donde se corra, deberá tener el framework .Net v4.6.1 para poder ejecutarlo

<b>Modo de uso: Nic.Galaxy.exe [OPCIONES]</b>
Opciones:
  -i, --iniciar=VALUE        Crea los próximo 10 años de las condiciones 
                               climática de la galáxia. Valores posibles de
                               parámetros: true=Forzar inicialización,
                               false=verifica si faltan datos, y en caso de
                               faltar, fuerza inicialización.
      --ev, --evulcano=VALUE Trae la cantidad de período del clima específico
                               del planeta vulcano. Posibles parámetros: Norma-
                               l, Sequia, Lluvia, Optimo
      --ef, --eferengi=VALUE Trae la cantidad de período del clima específico
                               del planeta vulcano. Posibles parámetros: Norma-
                               l, Sequia, Lluvia, Optimo
      --eb, --ebetasoide=VALUE
                             Trae la cantidad de período del clima específico
                               del planeta vulcano. Posibles parámetros: Norma-
                               l, Sequia, Lluvia, Optimo
  -c, --clima=VALUE          Trae el clima de un dia específico. Parámetro
                               numérico
  -?, -h, --help             Muestra esta ayuda y sale
 
