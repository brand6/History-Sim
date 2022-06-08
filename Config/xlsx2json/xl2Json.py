from operator import truediv
import xlwings as xw
import json
import pandas as pd
import numpy as np
import io
import os

app = xw.App(add_book=False)
app.visible = True
app.display_alerts = False

xlFloder = os.path.abspath(os.path.join(os.getcwd(),'..','excel'))
dataFloder = os.path.abspath(os.path.join(os.getcwd(),'../..','Assets/Data'))

xlFiles = os.listdir(xlFloder)

for xlName in xlFiles:
    xlPath = os.path.join(xlFloder,xlName)
    if xlName[-4:] == 'xlsx' and xlName[0]!= '~':
        xlFile = app.books.open(xlPath,update_links= False,read_only= True)
        for sht in xlFile.sheets:
            if sht.name[0] != '!':
                print(sht.name)
                usedList = sht.used_range.value
                titleList = usedList[1] # 第二行为标题
                dataMap={}
                mapList = []
                for r in range(2,len(usedList)): # 第三行开始正式数据
                    lineMap = {}
                    for c in range(len(titleList)):
                        if titleList[c] != None:
                            lineMap[titleList[c]] = usedList[r][c]
                    mapList.append(lineMap)
                dataMap[sht.name] = mapList
                
                jsonFile = io.open(os.path.join(dataFloder,sht.name + '.json'),'w',encoding='utf-8')
                jsonStr = json.dumps(dataMap,indent=2,ensure_ascii=False)
                jsonFile.write(jsonStr)
                jsonFile.close()
        xlFile.close()
app.quit()