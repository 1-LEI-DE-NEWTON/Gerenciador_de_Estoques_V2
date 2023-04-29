import mysql.connector
import pandas as pd
import xlsxwriter
import os

cnx = mysql.connector.connect(user='root', password='',
                              host='localhost', port='3306', database='gerenciadordeestoque')

query = "SELECT * FROM produtos"

df = pd.read_sql(query, con=cnx)
writer = pd.ExcelWriter('produtos.xlsx', engine='xlsxwriter')
df.to_excel(writer, sheet_name='Tabela de Produtos', index=False)

workbook = writer.book
worksheet = writer.sheets['Tabela de Produtos']
chart = workbook.add_chart({'type': 'pie'})

data_len = len(df)
chart.add_series({
    'name': 'Quantidade',
    'categories': f'=\'Tabela de Produtos\'!$A$2:$A${data_len + 1}',
    'values': f'=\'Tabela de Produtos\'!$C$2:$C${data_len + 1}',
})

chart.set_title({'name': 'Quantidade de Produtos'})
chart.set_legend({'position': 'right'})

worksheet.insert_chart('E2', chart)

for i, col in enumerate(df.columns):
    column_len = df[col].astype(str).str.len().max()
    column_len = max(column_len, len(col))
    worksheet.set_column(i, i, column_len)

writer.save()
#informa onde foi salvo

print('Arquivo salvo em: ', os.getcwd())
cnx.close()

#Espera o usuario digitar algo para sair
#input("Pressione qualquer tecla para sair...")
