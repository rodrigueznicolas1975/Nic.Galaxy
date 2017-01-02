# Nic.Galaxy
- Sistema inform�tico para la predicci�n del clima de la galaxia que conforman las tres civilizaciones: Vulcanos, Ferengis y Betasoides.

<b>Autor: </b>
- Rodriguez Nicolas

<b>Interpretaciones y aclaraciones de la premisas y Bonus:</b>

- Se toma como el sistema gal�ctivo empieza el d�a 0, en donde los 3 plantes y el sol, estan alineados y cuyo valor del coseno es 1 (cos(0) = 1).

- Los posibles estados clim�ticos son:
* Lluvia: Cuando los 3 Planetas forman un tri�ngulo y el Sol se encuentra dentro del per�metro. Y su pico de intensidad, cuando el per�metro est� en su m�ximo valor.
* Sequ�a: Cuando los 3 planetas y el sol, est�n alineados entre s�.
* �ptima: Cuando los 3 planetas est�n alineados entre s�, pero no el Sol, se encuentra �iptima de presi�n y temperatura.
* Normal: Cuando no se cumple con ninguna de los 3 escenarios arriba mencionados.
  
- En la premisas, habla de poder predecir en los pr�ximos 10 a�os. En este caso, cada planeta dura distintos d�as para cumplir un a�o, ya que un a�o, significa cuanto tarda el planeta en dar la vuelta completa al Sol. 
  Para calcular cuantos d�as tiene un a�o, y sabiendo que un a�o, ser�a dar la vuelta completa al sol y volver al punto inicial, sabiendo que nos basamos que el valor del coseno es 1, y el coseno de 360 es una vuelta entera, se divide 360 por la velocidad del planeta.
  Ejemplo: El Planeta Vulcano, tiene una velocidad de 5 grados/d�a, entonces, para dar una vuelta al sol, ser�a 360/5 = 72 d�as. Multiplicados por 10 a�os, se saca las estad�sticas de los primeros 720 d�as.

- El proyecto se encar� para que el sistema pudiera estar preparado para m�s de una galaxia (haciendo que sea flexible para agregar otra galaxia y programando su m�dulo para las predicciones, seg�n su regla).

- Se utiliz� el almacenamiento de una base relacional para guardar la galaxia, con sus planetas y sus predicciones de los primeros 10 a�os del planeta que m�s tarda en completar todo el ciclo.

- Se crearon 3 tipos de servicio:
* Inicializador: Para que el sistema calcule y almacene las predicciones de los tiempos.
* Predici�n: Recibiendo el nombre del planeta y el tiempo, el servicio retorna cuantos d�as/per�odos va a aparecer, y dato adiccional, en caso de lluvia, que d�a es de mayor intensidad.
* Clima: Recibiendo el d�a de la "galaxia", que clima se va a encontrar para los 3 plantes.
> Nota: Si se quiere utilizar la Predicci�n y el clima, sin haberse ejecutado previamente el Inicializador, se producir� una excepci�n de tipo Nic.Galaxy.Exception.ValidException pidiendo que se ejecute el Inicializador, asi tiene toda la informaci�n necesaria.

- Se cre� una api y un ejecutable para poder utilizar los servicios, obteniendo los mismos resultados.

---

<b>Caracter�sticas del proyecto:</b>

- El proyecto fue encarado y desarrollado con las siguientes tecnolog�a y packages: 
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
> El Job para inicializar las predicciones de la galaxia, true: forzando rehacer los c�lculos, false: verifica si tiene todo calculado, sino los vuelve a calcular. 
> Nota: Este REST, deber�a haber sido un POST, pero para facilitar la prueba, se lo configur� con GET, as� se pod�a facilitar la ejecuci�n del RES 

* [http://nicgalaxy.apphb.com/planeta/{planeta}/estadistica/{clima}]
> Devuelve la cantidad de d�as que tuvo en un planeta determinado <b>['vulcano', 'ferengis', 'betasoides']</b> y el tipo de clima <b>['normal', 'sequia', 'lluvia', 'optimo']</b>

* [http://nicgalaxy.apphb.com/estadistica/{clima}]
> Devuelve la cantidad de d�as que tuvo en el planeta vulcano y el tipo de clima <b>['normal', 'sequia', 'lluvia', 'optimo']</b>

* [http://nicgalaxy.apphb.com/clima?dia={numerodia}]
> Devuelve el clima seg�n el d�a de la galaxia.

---

<b>Ejecuci�n mediante ejecutable:</b>

- Como todo esta centrado en un servicio, se cre� tambien en el proyecto un ejecutable, en la cual, se puede ejecutar los mismos servicios que la api.
> Nota: En Github, se subieron el paquete compilado que se encuentra en el directorio "processes", lo que si, en donde se corra, deber� tener el framework .Net v4.6.1 para poder ejecutarlo

<b>Modo de uso: Nic.Galaxy.exe [OPCIONES]</b>
Opciones:
  -i, --iniciar=VALUE        Crea los pr�ximo 10 a�os de las condiciones 
                               clim�tica de la gal�xia. Valores posibles de
                               par�metros: true=Forzar inicializaci�n,
                               false=verifica si faltan datos, y en caso de
                               faltar, fuerza inicializaci�n.
      --ev, --evulcano=VALUE Trae la cantidad de per�odo del clima espec�fico
                               del planeta vulcano. Posibles par�metros: Norma-
                               l, Sequia, Lluvia, Optimo
      --ef, --eferengi=VALUE Trae la cantidad de per�odo del clima espec�fico
                               del planeta vulcano. Posibles par�metros: Norma-
                               l, Sequia, Lluvia, Optimo
      --eb, --ebetasoide=VALUE
                             Trae la cantidad de per�odo del clima espec�fico
                               del planeta vulcano. Posibles par�metros: Norma-
                               l, Sequia, Lluvia, Optimo
  -c, --clima=VALUE          Trae el clima de un dia espec�fico. Par�metro
                               num�rico
  -?, -h, --help             Muestra esta ayuda y sale
 
