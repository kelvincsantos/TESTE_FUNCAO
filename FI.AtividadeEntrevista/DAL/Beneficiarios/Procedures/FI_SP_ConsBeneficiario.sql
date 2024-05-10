﻿CREATE OR ALTER PROC FI_SP_ConsBeneficiario
	@IdCliente BIGINT
AS
BEGIN
	IF(ISNULL(@IdCliente,0) = 0)
		SELECT NOME, CPF, ID, IDCLIENTE FROM BENEFICIARIOS WITH(NOLOCK)
	ELSE
		SELECT NOME, CPF, ID, IDCLIENTE FROM BENEFICIARIOS WITH(NOLOCK) WHERE IDCLIENTE = @IDCLIENTE
END