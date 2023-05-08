# Desvendando a INVERSÃO DE DEPENDÊNCIA e o uso de interfaces com a ajuda dos TESTES DE UNIDADE

Solution criada como exemplo da aula sobre inversão de dependências. Link: https://youtu.be/y5fKQ3UqVJM

O exemplo não é 100% funcional (não acessa o banco e nem a api de cliente), mas compila e executa normalmente.

Se quiser testar a inversão, exclua os projetos Api e InfraNova, e tente compilar a solution. Veja que vai compilar normalmente, pois o core do sistema (Domain e Application) não depende do resto da solution - ou seja, é possível trocar as classes de infra por tecnologias diferentes ou mudar o output para um console application sem impactar as regras de negócio.

