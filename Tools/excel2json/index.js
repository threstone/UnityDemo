const fs = require('fs');
const path = require('path');
const Xls2json = require('./Xls2json');
const outputPath = path.join(__dirname, '../../configs/');

mkdirIfNotExist(outputPath);
mkdirIfNotExist(outputPath + "/server");
mkdirIfNotExist(outputPath + "/client");

start();
async function start() {
    const excelPath = path.join(__dirname, '../../excel/');
    const files = fs.readdirSync(excelPath)
    for (let index = 0; index < files.length; index++) {
        const filename = files[index];
        if (filename.startsWith('~') || filename.endsWith('.xlsx') === false) {
            continue
        }

        await writeExcel(excelPath + filename, outputPath);
        console.log('完成', filename);
    }

    require('../config_merge/merge')
}

async function writeExcel(xlsxPath) {
    const data = new Xls2json(xlsxPath, 3, 4, 5, 6, 7);
    const sheetConfigs = await data.fromFileAsync();

    const sheetKeys = Object.keys(sheetConfigs);
    for (let index = 0; index < sheetKeys.length; index++) {
        const sheetName = sheetKeys[index];
        writeSheet(sheetName, sheetConfigs[sheetName]);
    }
}

function writeSheet(sheetName, config) {
    let c, s;
    if (config.isMap) {
        c = config.dataList;
        s = config.dataList;
    } else {
        c = [];
        s = [];
        const csMap = {};
        for (let index = 0; index < config.varList.length; index++) {
            const key = config.varList[index];
            csMap[key] = {
                isC: config.csList[index].indexOf('c') !== -1,
                isS: config.csList[index].indexOf('s') !== -1
            }
        }
        for (const key in config.dataList) {
            const value = config.dataList[key];
            const tempC = {};
            let hasC = false;
            const tempS = {};
            let hasS = false;

            for (let index = 0; index < config.varList.length; index++) {
                const keyOfObj = config.varList[index];
                const csData = csMap[keyOfObj];
                if (csData.isC) {
                    tempC[keyOfObj] = value[keyOfObj];
                    hasC = true;
                }
                if (csData.isS) {
                    tempS[keyOfObj] = value[keyOfObj];
                    hasS = true;
                }
            }

            if (hasC) {
                c[key] = tempC;
            }
            if (hasS) {
                s[key] = tempS;
            }
        }
    }
	console.log(sheetName);
    fs.writeFileSync(outputPath + 'client/' + sheetName + '.json', JSON.stringify(c));
    fs.writeFileSync(outputPath + 'server/' + sheetName + '.json', JSON.stringify(s));
}

function mkdirIfNotExist(dirPath) {
    try {
        fs.statSync(dirPath);
    } catch (error) {
        fs.mkdirSync(dirPath);
    }
}