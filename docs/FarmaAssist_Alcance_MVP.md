# FarmaAssist --- Alcance del MVP

**Tipo de producto:** plataforma web de asistencia y gestión
farmacéutica\
**Nombre del producto:** FarmaAssist\
**Versión:** 0.6\
**Fecha:** 11 de noviembre de 2026\
**Estado:** propuesta inicial para descubrimiento y validación

------------------------------------------------------------------------

## 1. Nombre del producto

### FarmaAssist

**Concepto:** asiste la operación de la farmacia, organizando la
información del paciente, la atención, los documentos y el inventario
sin reemplazar al profesional de salud.

**Por qué funciona:** "Assist" comunica apoyo al personal, en línea con
el principio de que el sistema no reemplaza al profesional.

**Descriptor sugerido:** *Gestión y asistencia para farmacias.*

Antes de uso comercial, se debe verificar disponibilidad de marca,
dominio y nombres similares en los canales correspondientes.

------------------------------------------------------------------------

## 2. Visión del producto

FarmaAssist será una aplicación web para ayudar al personal de una
farmacia a organizar la atención de sus clientes, consultar antecedentes
registrados, procesar documentos aportados por el paciente y verificar
la disponibilidad de medicamentos en el inventario.

El MVP se enfocará en **gestión de información y apoyo operativo**, no
en diagnosticar enfermedades, prescribir tratamientos ni sustituir el
criterio de profesionales de salud.

### Problema que busca resolver

En muchas farmacias, la atención se apoya en conversaciones informales,
documentos físicos y conocimiento individual del empleado. Esto
dificulta: - Consultar antecedentes que el cliente ya había informado. -
Mantener un registro organizado de las atenciones. - Leer y transcribir
información de fórmulas y documentos. - Encontrar rápidamente un
producto en el inventario. - Identificar cuándo la situación reportada
requiere remisión a un servicio de salud.

### Objetivo del MVP

Entregar una primera versión funcional que permita registrar pacientes y
atenciones, administrar un inventario importado desde Excel, adjuntar
documentos y utilizar IA para extraer y organizar información documental
con validación humana.

------------------------------------------------------------------------

## 3. Usuarios y roles

### Usuario principal: empleado de farmacia

Puede: - Registrar y consultar pacientes. - Crear registros de
atención. - Consultar antecedentes previamente registrados. - Adjuntar
documentos. - Revisar y confirmar información extraída por IA. -
Consultar el inventario y la disponibilidad de productos.

### Administrador de farmacia

Puede: - Crear y administrar usuarios. - Importar y actualizar el
inventario. - Consultar registros y auditoría según sus permisos. -
Configurar datos básicos del establecimiento.

### Consideración

FarmaAssist es un SaaS multi-tenant: cada farmacia es un tenant con sus
usuarios, pacientes, atenciones, documentos e inventario aislados de los
demás. Droguería Villacolombia (Cali) es la farmacia piloto.

El MVP incluye desde el inicio el aislamiento por tenant, el alta y la
configuración de la farmacia, y un panel para que la farmacia supervise su
espacio de trabajo (usuarios, auditoría y estado del inventario). El portal
de autoservicio para nuevas farmacias, la facturación y los planes quedan
para una fase posterior.

------------------------------------------------------------------------

## 4. Alcance funcional del MVP

### 4.1. Autenticación y usuarios

**Incluye:** - Inicio y cierre de sesión. - Roles básicos: administrador
y empleado. - Protección de rutas y operaciones según rol. - Registro de
fecha, usuario y acción en operaciones relevantes.

**No incluye:** - Inicio de sesión social. - Integración con directorios
corporativos. - Administración compleja de permisos personalizados.

### 4.2. Gestión de pacientes

Permite crear, consultar, editar y visualizar pacientes.

**Campos iniciales:**

  ------------------------------------------------------------------------
  Campo                                    Requerido Observaciones
  --------------------- ---------------------------- ---------------------
  Nombre completo                                 Sí Nombre informado por
                                                     el paciente

  Tipo de documento                               Sí Según documentos
                                                     admitidos por el
                                                     establecimiento

  Número de documento                             Sí Validar duplicados

  Teléfono                                        No Dato de contacto

  Fecha de nacimiento                             No Útil para
                                                     contextualizar la
                                                     atención

  Alergias reportadas                             No Debe permitir indicar
                                                     "desconocido"

  Enfermedades                                    No Información declarada
  preexistentes                                      o documentada

  Medicamentos                                    No Información declarada
  habituales                                         o documentada

  Observaciones                                   No Texto libre con
                                                     acceso controlado
  ------------------------------------------------------------------------

**Reglas:** - El tipo y número de documento deben permitir localizar un
paciente existente. - No se deben crear duplicados silenciosamente. -
Los datos clínicos deben distinguir entre información declarada por el
paciente, extraída de documentos y verificada por una persona. - La
ausencia de información no debe interpretarse como ausencia de alergias
o enfermedades.

### 4.3. Registro de atenciones

Cada atención se registra como un evento independiente asociado a un
paciente.

**Campos:** - Fecha y hora. - Motivo de consulta. - Síntomas descritos
por el paciente, si aplica. - Tiempo de evolución, si se conoce. -
Antecedentes relevantes. - Observaciones del empleado. - Documentos
asociados. - Resultado operativo: información entregada, consulta al
profesional correspondiente o remisión sugerida.

**Reglas:** - Debe ser posible registrar una atención basada en
síntomas, documentos aportados o ambos. - No exigir síntomas cuando el
motivo sea revisar una fórmula u otro documento. - No presentar la
información registrada como diagnóstico médico. - Mantener un historial
cronológico de atenciones.

### 4.4. Gestión e importación de inventario

> **Nota:** esta sección está sujeta a cambios de acuerdo con la información
> que entregue Droguería Villacolombia (campos, calidad y estructura del
> export de inventario).

El inventario inicial se cargará desde el archivo Excel entregado por la
farmacia.

**Funciones:** - Cargar el Excel. - Mostrar una vista previa de filas y
columnas. - Mapear columnas del archivo a campos internos. - Validar
datos obligatorios y reportar filas con errores. - Detectar posibles
duplicados. - Confirmar la importación antes de modificar el
inventario. - Consultar y buscar productos.

**Modelo mínimo sugerido del producto:**

  Campo                              Requerido en el MVP
  ----------------------- ------------------------------
  Identificador interno                               Sí
  Nombre comercial                                    Sí
  Principio activo                    Si está disponible
  Concentración                       Si está disponible
  Forma farmacéutica                  Si está disponible
  Presentación                        Si está disponible
  Existencias               Sí, si el Excel las contiene
  Precio                              Si está disponible
  Código o referencia                 Si está disponible
  Registro sanitario                  Si está disponible
  Estado                                              Sí

**Reglas:** - El sistema debe conservar el archivo original como
referencia de importación, con acceso restringido. - La importación debe
producir un reporte de éxitos, advertencias y errores. - No inferir
equivalencia terapéutica únicamente por similitud del nombre. - Si el
Excel no contiene principio activo, concentración o forma farmacéutica,
mostrar esa limitación y no asumir coincidencias.

**Fuera del MVP:** - Punto de venta y facturación. - Integración con
proveedores. - Compras y órdenes de abastecimiento. - Control avanzado
de lotes y vencimientos, salvo que el archivo y la operación piloto
permitan incorporarlos de forma sencilla.

### 4.5. Carga y procesamiento de documentos

El empleado podrá adjuntar documentos aportados por el paciente, por
ejemplo: - Fórmulas o prescripciones médicas. - Informes de
laboratorio. - Informes médicos. - Informes radiológicos.

**Flujo propuesto:** 1. Seleccionar o crear el paciente. 2. Adjuntar el
documento. 3. Almacenar el archivo de forma privada. 4. Ejecutar OCR y
extracción estructurada cuando el tipo de archivo sea compatible. 5.
Mostrar el texto y los campos extraídos. 6. Permitir corrección y
confirmación humana. 7. Asociar el resultado confirmado al paciente y a
la atención correspondiente.

**Datos que se pueden extraer de una fórmula para revisión:** - Nombre
del medicamento. - Principio activo, si aparece. - Concentración. -
Forma farmacéutica. - Instrucciones de uso tal como aparecen en la
fórmula. - Fecha y datos del prescriptor, si son legibles.

**Reglas de seguridad:** - La IA no debe completar campos ilegibles
mediante suposiciones. - Los campos inciertos deben marcarse como
"requiere revisión". - La extracción automática no equivale a validar la
autenticidad o vigencia de una prescripción. - El sistema no debe
modificar ni reinterpretar las instrucciones prescritas. - Los
documentos originales deben permanecer disponibles para verificación por
usuarios autorizados.

**Limitación del MVP:** priorizar PDF e imágenes legibles. La
interpretación automatizada de radiografías o el diagnóstico a partir de
imágenes médicas queda expresamente excluido. Para radiografías, el
sistema podrá almacenar el archivo y, si existe, procesar el informe
escrito adjunto.

### 4.6. Asistencia con IA y reglas de seguridad

La IA tendrá un papel acotado y supervisado.

**Incluye:** - Resumir información ingresada en la atención. - Organizar
datos extraídos de documentos. - Señalar información faltante o
contradictoria. - Ayudar a localizar términos y medicamentos mencionados
en una fórmula. - Presentar advertencias generales basadas en reglas
verificadas y fuentes autorizadas, cuando estén disponibles. -
Identificar situaciones que deben escalarse a un profesional de salud o
a un servicio de urgencias, según protocolos definidos y validados por
profesionales competentes.

**No incluye:** - Emitir diagnósticos médicos. - Recomendar un
tratamiento personalizado a partir de síntomas. - Calcular o prescribir
dosis. - Recomendar iniciar, suspender o modificar medicamentos. -
Determinar por sí sola que un medicamento es seguro para una persona. -
Sustituir una prescripción. - Presentar una respuesta generada por IA
como una decisión clínica validada.

La interfaz debe diferenciar claramente: - Información declarada por el
paciente. - Información extraída automáticamente. - Información
confirmada por el empleado. - Información general proporcionada por
fuentes documentales.

### 4.7. Consulta de medicamentos de una fórmula

El sistema podrá comparar los medicamentos extraídos de una fórmula con
los productos registrados en el inventario.

**Flujo:** 1. El empleado carga la fórmula. 2. Revisa y confirma los
medicamentos extraídos. 3. El sistema busca coincidencias en el
inventario. 4. Presenta disponibilidad y atributos del producto. 5. Si
no encuentra una coincidencia, informa que no está disponible o que los
datos son insuficientes.

**Reglas:** - Comparar, cuando estén disponibles, principio activo,
concentración, forma farmacéutica y presentación. - Distinguir
coincidencia exacta, coincidencia parcial y ausencia de coincidencia. -
No declarar equivalencia terapéutica por coincidencia de nombre o
principio activo solamente. - No proponer sustituciones automáticas. -
Cualquier alternativa debe seguir el procedimiento legal aplicable y
requerir la intervención del profesional autorizado y, cuando
corresponda, del prescriptor.

### 4.8. Historial y auditoría

**Incluye:** - Historial cronológico de atenciones por paciente. -
Registro de usuario, fecha y operación en acciones sensibles. -
Trazabilidad de importaciones de inventario. - Trazabilidad de
documentos procesados y correcciones de datos extraídos.

------------------------------------------------------------------------

### 4.9. Pantalla de atención (ajustes del mockup inicial)

El mockup v0.1 se usa como referencia visual. Estos ajustes son obligatorios
para que la pantalla cumpla el alcance:

- **Nombre:** FarmaAssist es el nombre oficial. Unificar en toda la
  interfaz y en la documentación.
- **Terminología:** reemplazar "IA CLINICAL" y "Triaje clínico" por
  "Asistente documental" y "Registro de atención". No se presenta función
  clínica.
- **Acciones:** el botón "Generar triaje clínico y consultar medicamentos"
  se separa en "Guardar atención" (MVP) y "Consultar inventario" (fase
  posterior, sección 4.7).
- **Salidas excluidas:** no se muestran recomendaciones farmacológicas ni
  sugerencias de medicamentos a partir de síntomas (sección 4.6).
- **Identificación:** "DNI" se sustituye por tipo y número de documento
  (sección 4.2). "Edad" se sustituye por fecha de nacimiento.
- **Género:** se retira del MVP salvo justificación legal documentada
  (sección 7, minimización de datos).
- **Enfermedades y alergias:** campos estructurados con opciones "Ninguna
  conocida" y "Desconocido". Un campo vacío no equivale a ausencia de
  alergias.
- **Carga de archivos:** en el MVP se almacena el archivo. El distintivo
  "Visión IA OCR" se muestra como "Pendiente" hasta la fase de OCR. Las
  radiografías solo se almacenan, sin interpretación.
- **Aviso:** el banner de orientación se conserva. La referencia a
  urgencias debe seguir el protocolo aprobado (sección 4.6).
- **Campos ausentes en el mockup:** fecha y hora de la atención, motivo de
  consulta, resultado operativo, usuario que registra y origen de cada dato
  (declarado, extraído o confirmado).

---

## 5. Flujo principal del usuario

1.  El empleado inicia sesión.
2.  Busca al paciente por tipo y número de documento.
3.  Si existe, revisa sus datos y antecedentes; si no existe, registra
    los datos mínimos.
4.  Crea una nueva atención.
5.  Registra los síntomas o adjunta el documento que motiva la consulta.
6.  Si hay documento, revisa y confirma la información extraída.
7.  El sistema muestra un resumen organizado y las alertas definidas por
    las reglas aprobadas.
8.  Si existe una fórmula, consulta los medicamentos en el inventario.
9.  El empleado revisa la información y sigue el procedimiento de
    atención correspondiente.
10. Guarda el resultado de la atención.

------------------------------------------------------------------------

## 6. Fuera del alcance del MVP

Para evitar riesgos y controlar el esfuerzo inicial, quedan excluidos:

-   Diagnóstico médico automatizado.
-   Prescripción o recomendación autónoma de tratamientos.
-   Sustitución automática de medicamentos formulados.
-   Interpretación diagnóstica de radiografías o imágenes clínicas.
-   Historia clínica interoperable con IPS, EPS o sistemas externos.
-   Integración con sistemas de facturación o punto de venta.
-   Aplicación móvil nativa.
-   Portal de pacientes.
-   Chatbot público para autodiagnóstico.
-   Telemedicina.
-   Analítica predictiva sobre pacientes.
-   Entrenamiento de modelos propios con datos de pacientes.
-   Comercialización SaaS con pagos y planes automatizados.

------------------------------------------------------------------------

## 7. Requisitos no funcionales y protección de datos

La información de salud es sensible. La protección de datos debe ser un
requisito transversal, no una funcionalidad posterior.

**Requisitos mínimos:** - Autorización y fundamento legal adecuados para
el tratamiento de datos personales y sensibles. - Aviso y política de
tratamiento de datos. - Recolección limitada a información necesaria
para la finalidad definida. - Acceso por roles y principio de mínimo
privilegio. - Cifrado en tránsito y en reposo. - Almacenamiento privado
de documentos, sin enlaces públicos permanentes. - Auditoría de
consultas y modificaciones relevantes. - Copias de seguridad y
procedimiento de recuperación. - Política de retención y eliminación de
información. - Gestión de incidentes de seguridad. - Revisión de
contratos, ubicación, transferencias y condiciones de tratamiento de los
proveedores de OCR e IA. - No utilizar información identificable de
pacientes para entrenar modelos externos.

Antes de un piloto con datos reales, se debe validar el tratamiento de
datos con asesoría jurídica y revisar las obligaciones aplicables al
establecimiento y al tipo de servicio prestado.

También se debe validar con un profesional farmacéutico y asesoría
regulatoria el alcance permitido del software, las funciones del
personal usuario y las reglas de dispensación aplicables en Colombia.

------------------------------------------------------------------------

## 8. Arquitectura técnica inicial sugerida

Se recomienda un monolito modular para el MVP.

  -----------------------------------------------------------------------
  Capa                                Tecnología propuesta
  ----------------------------------- -----------------------------------
  Frontend                            Angular

  Backend                             ASP.NET Core Web API

  Persistencia                        PostgreSQL

  Acceso a datos                      Entity Framework Core

  Autenticación                       ASP.NET Core Identity

  Archivos                            Almacenamiento privado compatible
                                      con Azure Blob Storage

  OCR                                 Servicio de OCR / Document
                                      Intelligence

  IA                                  Qwen mediante Ollama, en infraestructura
                                      controlada, sin proveedor externo

  Procesos asíncronos                 Background Services o Azure
                                      Functions, según necesidad

  Despliegue                          Piloto: despliegue local (API, PostgreSQL y
                                      Ollama en servidor dedicado). Productivo:
                                      pendiente de proveedor y contrato de datos
  -----------------------------------------------------------------------

### Módulos de dominio sugeridos

-   Identity
-   Patients
-   Consultations
-   Documents
-   Inventory
-   PrescriptionReview
-   AIOrchestration
-   Audit

La IA debe estar detrás de interfaces de aplicación y servicios
controlados. Las reglas críticas de seguridad y validación no deben
depender exclusivamente de instrucciones de un prompt.

### Entidades iniciales sugeridas

-   User
-   Pharmacy
-   Patient
-   PatientAllergy
-   PatientCondition
-   PatientMedication
-   Consultation
-   ConsultationSymptom
-   Document
-   ExtractedDocumentData
-   InventoryProduct
-   InventoryImport
-   PrescriptionItem
-   AuditEvent

Aunque el piloto empiece con una sola farmacia, cada dato operativo debe
quedar asociado al establecimiento (`PharmacyId`) para facilitar un
futuro modelo multi-tenant y evitar mezclar información.

------------------------------------------------------------------------

## 9. Criterios de aceptación del MVP

El MVP podrá considerarse listo para una prueba piloto cuando:

1.  Un usuario autorizado pueda iniciar sesión y acceder solo a las
    funciones de su rol.
2.  Se pueda crear, consultar y actualizar un paciente sin duplicarlo
    por documento.
3.  Se pueda registrar más de una atención para el mismo paciente.
4.  Se pueda consultar el historial cronológico de atenciones.
5.  Se pueda importar el Excel de inventario con vista previa y reporte
    de errores.
6.  Se puedan buscar productos por nombre y, cuando exista, por
    principio activo.
7.  Se puedan adjuntar documentos a un paciente y a una atención.
8.  La extracción documental presente los datos detectados y permita
    corregirlos antes de confirmarlos.
9.  Los campos ilegibles o inciertos se identifiquen sin inventar
    valores.
10. Una fórmula confirmada pueda compararse con el inventario y mostrar
    coincidencias y disponibilidad.
11. El sistema no diagnostique, prescriba ni sustituya automáticamente
    medicamentos.
12. Las operaciones sensibles queden auditadas.
13. Los documentos y datos sensibles no sean accesibles públicamente.
14. Se hayan validado los avisos, autorizaciones, políticas y
    condiciones de tratamiento de datos antes de usar información real.

------------------------------------------------------------------------

## 10. Plan de implementación por entregas

### Entrega 0 --- Descubrimiento y validación

-   Revisar el Excel real.
-   Identificar columnas, calidad y consistencia del inventario.
-   Entrevistar a la persona que atenderá con el sistema.
-   Validar el flujo de trabajo real.
-   Definir protocolos de escalamiento y límites de uso con un
    profesional competente.
-   Revisar privacidad y obligaciones regulatorias.

### Entrega 1 --- Núcleo operativo

-   Proyecto frontend y backend.
-   Autenticación y roles.
-   CRUD de pacientes.
-   Registro e historial de atenciones.
-   Importación y consulta de inventario.

### Entrega 2 --- Documentos

-   Carga privada de archivos.
-   OCR.
-   Extracción estructurada.
-   Pantalla de revisión y confirmación.
-   Asociación al paciente y a la atención.

### Entrega 3 --- Asistencia y fórmulas

-   Resumen de atención con IA.
-   Reglas y alertas revisadas por profesionales.
-   Extracción de medicamentos de fórmulas.
-   Búsqueda y comparación contra inventario.
-   Auditoría y pruebas de seguridad.

### Entrega 4 --- Piloto controlado

-   Pruebas con usuarios.
-   Datos de prueba inicialmente.
-   Capacitación.
-   Validación de permisos y procedimientos.
-   Evaluación de precisión de OCR y utilidad de la interfaz.
-   Autorización formal antes de operar con datos reales.

------------------------------------------------------------------------

## 11. Decisiones pendientes para iniciar el desarrollo

1.  **Tipo de establecimiento:** confirmar si es droguería, farmacia u
    otro establecimiento y qué profesionales están vinculados.
2.  **Usuarios reales:** confirmar quién utilizará el sistema y qué
    funciones puede realizar.
3.  **Inventario:** revisar el Excel para determinar campos disponibles,
    identificadores y frecuencia de actualización.
4.  **Documentos:** definir formatos y calidad esperada de fórmulas y
    resultados.
5.  **Fuentes farmacológicas:** seleccionar fuentes oficiales y
    confiables para la información que se presente.
6.  **Privacidad:** definir responsable del tratamiento, finalidad,
    autorizaciones, retención y proveedores.
7.  **Infraestructura:** confirmar presupuesto, alojamiento y proveedor
    de OCR/IA.
8.  **Alcance comercial:** decidir si el primer piloto será exclusivo
    para una farmacia o si desde el comienzo se probará con más
    establecimientos.

------------------------------------------------------------------------

## 12. Principio rector del producto

**FarmaAssist organiza información y apoya la operación farmacéutica; no
reemplaza al médico, al prescriptor ni al profesional farmacéutico
autorizado.**

Toda salida de IA debe ser trazable, revisable y presentada con sus
limitaciones. La seguridad del paciente y el cumplimiento normativo
prevalecen sobre la automatización o la venta de un producto.
