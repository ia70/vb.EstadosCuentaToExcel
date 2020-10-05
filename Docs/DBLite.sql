-- -----------------------------------------------------
-- Table `empresa_externa`
-- -----------------------------------------------------
CREATE TABLE `empresa_externa` (
	`idempresa_externa`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`razon_social`	TEXT,
	`rfc`	TEXT,
	`descripcion`	TEXT
);


-- -----------------------------------------------------
-- Table `vinculo`
-- -----------------------------------------------------
CREATE TABLE `vinculo` (
  `idvinculo` INTEGER PRIMARY KEY AUTOINCREMENT,
  `idempresa_externa` INTEGER,
  `no_cuenta` TEXT
);

-- -----------------------------------------------------
-- Table `tr_procesada`
-- -----------------------------------------------------
CREATE TABLE `tr_procesada` (
  `clave_rastreo` TEXT PRIMARY KEY
);

-- -----------------------------------------------------
-- Table `transaccion`
-- -----------------------------------------------------
CREATE TABLE `transaccion` (
  `idtransaccion` TEXT PRIMARY KEY,
  `idformato` TEXT,
  `c0` TEXT,
  `c1` TEXT,
  `c2` TEXT,
  `c3` TEXT,
  `c4` TEXT,
  `c5` TEXT,
  `c6` TEXT,
  `c7` TEXT,
  `c8` TEXT,
  `c9` TEXT,
  `c10` TEXT,
  `c11` TEXT,
  `c12` TEXT,
  `c13` TEXT,
  `c14` TEXT,
  `c15` TEXT,
  `c16` TEXT
);


-- -----------------------------------------------------
-- Table `empleados`
-- -----------------------------------------------------
CREATE TABLE `empleados` (
  `idempleados` INTEGER PRIMARY KEY AUTOINCREMENT,
  `idtransaccion` TEXT,
  `no_empleado` TEXT,
  `nombre` TEXT,
  `tipo_cuenta` TEXT,
  `no_cuenta` TEXT,
  `importe` TEXT,
  `descripcion` TEXT
  );


-- -----------------------------------------------------
-- Table `banco`
-- -----------------------------------------------------
CREATE TABLE `banco` (
  `no_cuenta` TEXT PRIMARY KEY,
  `nombre_banco` TEXT,
  `nombre_cuenta` TEXT,
  `rfc` TEXT,
  `moneda` TEXT,
  `tipo` TEXT
  );


-- -----------------------------------------------------
-- Table `formato`
-- -----------------------------------------------------
CREATE TABLE `formato` (
  `idformato` TEXT PRIMARY KEY,
  `identificador` TEXT,
  `nombre_transaccion` TEXT,
  `moneda` TEXT,
  `empleados` TEXT,
  `banco_origen` TEXT,
  `banco_destino` TEXT
  );


-- -----------------------------------------------------
-- Table `campos_nombre`
-- -----------------------------------------------------
CREATE TABLE `campos_nombre` (
  `idformato` TEXT PRIMARY KEY,
  `c0` TEXT,
  `c1` TEXT,
  `c2` TEXT,
  `c3` TEXT,
  `c4` TEXT,
  `c5` TEXT,
  `c6` TEXT,
  `c7` TEXT,
  `c8` TEXT,
  `c9` TEXT,
  `c10` TEXT,
  `c11` TEXT,
  `c12` TEXT,
  `c13` TEXT,
  `c14` TEXT,
  `c15` TEXT,
  `c16` TEXT
  );


-- -----------------------------------------------------
-- Table `campos_inicio`
-- -----------------------------------------------------
CREATE TABLE `campos_inicio` (
  `idformato` TEXT PRIMARY KEY,
  `c0` TEXT,
  `c1` TEXT,
  `c2` TEXT,
  `c3` TEXT,
  `c4` TEXT,
  `c5` TEXT,
  `c6` TEXT,
  `c7` TEXT,
  `c8` TEXT,
  `c9` TEXT,
  `c10` TEXT,
  `c11` TEXT,
  `c12` TEXT,
  `c13` TEXT,
  `c14` TEXT,
  `c15` TEXT,
  `c16` TEXT
  );
-- -----------------------------------------------------
-- Table `campos_fin`
-- -----------------------------------------------------
CREATE TABLE `campos_fin` (
  `idformato` TEXT PRIMARY KEY,
  `c0` TEXT,
  `c1` TEXT,
  `c2` TEXT,
  `c3` TEXT,
  `c4` TEXT,
  `c5` TEXT,
  `c6` TEXT,
  `c7` TEXT,
  `c8` TEXT,
  `c9` TEXT,
  `c10` TEXT,
  `c11` TEXT,
  `c12` TEXT,
  `c13` TEXT,
  `c14` TEXT,
  `c15` TEXT,
  `c16` TEXT
  );
