# FarmaAssist --- Propuesta de MVP y etapas

**Versión:** 0.4\
**Fecha:** 11 de noviembre de 2026\
**Documento de referencia:** [FarmaAssist_Alcance_MVP.md](FarmaAssist_Alcance_MVP.md)\
**Inventario analizado:** [21julioventas.xlsx](21julioventas.xlsx)

------------------------------------------------------------------------

## 1. Objetivo

Llegar lo antes posible a un MVP usable por la farmacia piloto, con datos
de prueba, que cubra el flujo principal: paciente, atención, documento
adjunto e inventario consultable.

## 2. Ajustes al alcance

Para acelerar el MVP se separa el alcance en **MVP** y **fase
posterior**.

| Área | MVP | Fase posterior |
|---|---|---|
| Autenticación y roles | Sí | |
| Multi-tenant: aislamiento de datos por farmacia | Sí | |
| Alta y configuración de farmacia (piloto Droguería Villacolombia) | Sí | |
| Panel de supervisión de la farmacia (usuarios, auditoría, inventario) | Sí | |
| Portal de autoservicio para nuevas farmacias, facturación y planes | | Sí |
| Pacientes (alta, búsqueda, sin duplicados por documento) | Sí | |
| Atenciones e historial cronológico | Sí | |
| Inventario: importación con vista previa y reporte | Sí | |
| Búsqueda de productos por nombre y código | Sí | |
| Adjuntar documentos (almacenamiento privado) | Sí | |
| Auditoría de acciones sensibles | Sí (versión básica) | Completa |
| OCR y extracción estructurada | | Sí |
| Resumen de atención con IA | | Sí |
| Comparación de fórmulas contra inventario | | Sí |
| Búsqueda por principio activo y disponibilidad | | Cuando la fuente de datos exista |

Motivo: el OCR, la IA y la comparación de fórmulas dependen de decisiones
pendientes (proveedor, tratamiento de datos, fuentes farmacológicas y
validación profesional). El núcleo operativo puede entregarse antes sin
depender de ellas.

## 2.1 Revisión del mockup inicial

El mockup de la pantalla de atención necesita estos ajustes antes de
diseño final (detalle en la sección 4.9 del alcance):

- Mantener el nombre FarmaAssist en la interfaz y la documentación.
- Retirar la terminología clínica ("triaje clínico", "IA CLINICAL").
- Quitar las recomendaciones farmacológicas de la pantalla de atención.
- Usar tipo y número de documento en lugar de DNI, y fecha de nacimiento en
  lugar de edad. Retirar género del MVP.
- Alergias y enfermedades con opciones estructuradas y "Desconocido".
- Mostrar el distintivo de OCR como pendiente hasta la Etapa 3.
- Agregar fecha y hora, motivo de consulta, resultado operativo y origen de
  cada dato.

## 3. Hallazgos del inventario

- 16.383 productos con código interno único (sirve como identificador).
- 393 productos sin código de barras principal.
- 292 códigos de barras repetidos y 55 nombres repetidos: se reportan
  como advertencias en la importación, no se bloquean.
- 651 productos con precio de lista 1 en cero: advertencia.
- El archivo no contiene principio activo, concentración, forma
  farmacéutica, registro sanitario, existencias ni estado. Sin
  existencias no se puede mostrar disponibilidad.
- Concentración y presentación aparecen dentro del nombre comercial;
  extraerlas requiere validación del personal farmacéutico.

## 4. Etapas

### Etapa 0 --- Decisiones y datos (inicio inmediato)

- Confirmar nombre FarmaAssist y descriptor.
- Confirmar a Droguería Villacolombia como farmacia piloto y designar su
  responsable.
- Decidir el modelo de datos multi-tenant: base compartida con `TenantId`
  (recomendado para el MVP) o base dedicada por farmacia.
- Solicitar a la farmacia un export con principio activo, concentración,
  forma, presentación, existencias, estado y fuente de actualización.
- Definir lista de tipos de documento admitidos.
- Designar responsable del tratamiento de datos y revisar el aviso de
  privacidad con asesoría jurídica.
- Confirmar con un profesional farmacéutico el alcance permitido del
  software.
- Definir infraestructura: piloto local (API, PostgreSQL y Ollama en un
  servidor dedicado) y evaluación de alojamiento productivo con contrato de
  datos.

**Salida:** decisiones registradas y datos de inventario completos.

### Etapa 1 --- Núcleo operativo (MVP, primera entrega)

- Proyecto base: Angular y ASP.NET Core Web API, monolito modular,
  PostgreSQL con Entity Framework Core (Npgsql).
- Autenticación con ASP.NET Core Identity y roles administrador y
  empleado.
- Multi-tenant: `TenantId` en todas las tablas de negocio, filtro
  obligatorio en el acceso a datos y contexto de tenant resuelto desde la
  sesión.
- Alta de farmacia: datos del establecimiento, usuario administrador
  inicial y parámetros de configuración.
- Panel de supervisión para el administrador de la farmacia: usuarios,
  auditoría y estado de la carga de inventario.
- Pacientes: alta, edición y búsqueda por tipo y número de documento;
  validación de duplicados.
- Atenciones: registro independiente por paciente, historial cronológico.
- Inventario: carga del Excel, vista previa, mapeo de columnas, reporte
  de éxitos, advertencias y errores, confirmación antes de guardar,
  conservación del archivo original con acceso restringido.
- Auditoría básica: usuario, fecha y acción en operaciones sensibles.
- Pantalla de atención con origen de cada dato (declarado, extraído o
  confirmado) y banner de orientación.

**Salida:** sistema funcional con datos de prueba, desplegado en el
entorno piloto.

### Etapa 2 --- Documentos adjuntos (MVP, segunda entrega)

- Carga de archivos PDF e imágenes con almacenamiento privado, sin
  enlaces públicos.
- Asociación del documento a paciente y a atención.
- Descarga restringida por rol.
- Sin OCR en esta etapa: el personal transcribe los datos manualmente.
- Distintivo de OCR marcado como pendiente en la pantalla de carga.

**Salida:** documentos asociados y accesibles solo a usuarios autorizados.

### Etapa 3 --- OCR y extracción (fase posterior)

- Integrar el servicio de OCR elegido tras revisar contrato y ubicación de datos.
- Pantalla de revisión: campos extraídos, marcados como "requiere
  revisión" cuando sean inciertos, con confirmación humana.
- Trazabilidad de correcciones.

### Etapa 4 --- Asistencia y fórmulas (fase posterior)

- Comparación de medicamentos confirmados contra el inventario, con
  coincidencia exacta, parcial o ausente.
- Resumen de atención con IA usando Qwen local (Ollama), detrás de
  interfaces controladas y sin
  datos identificables para entrenamiento externo.
- Reglas de alerta aprobadas por profesionales competentes.

### Etapa 5 --- Preparación para operar con datos reales

- Revisión de seguridad y pruebas de permisos.
- Pruebas automatizadas de aislamiento entre farmacias.
- Copias de seguridad y procedimiento de recuperación verificados.
- Política de retención y gestión de incidentes documentadas.
- Autorización formal del responsable antes de cargar datos reales.

## 5. Riesgos

| Riesgo | Mitigación |
|---|---|
| Inventario sin existencias ni principio activo | Solicitar export complementario en la Etapa 0 |
| Retraso en decisiones legales o de privacidad | Iniciar la Etapa 0 en paralelo con la Etapa 1 |
| Alcance de IA excesivo para el primer despliegue | Mantener IA fuera del MVP |
| Qwen local no alcanza la precisión requerida en terminología farmacéutica | Evaluar con casos anonimizados antes de la Etapa 4; si no cumple, probar un modelo mayor |
| La IA se usa como decisor y no como apoyo | Reglas críticas en código determinista; la IA solo resume, señala y sugiere revisión humana |
| Datos de salud sin contrato con proveedores | No usar OCR ni IA con datos reales hasta cerrar contratos |
| Fuga de datos entre farmacias | Filtro de tenant en todas las consultas, pruebas de aislamiento y ningún acceso sin tenant resuelto |

## 6. Próximos pasos

1. Confirmar esta propuesta, el nombre FarmaAssist y el modelo multi-tenant con Droguería Villacolombia como piloto.
2. Solicitar el export complementario de inventario.
3. Iniciar la Etapa 1 con la estructura del proyecto.
4. Actualizar el mockup según la sección 2.1 y la imagen consolidada.
