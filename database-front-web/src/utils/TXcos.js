// https://cloud.tencent.com/document/product/436/8629

import COS from 'cos-js-sdk-v5'
var cos = new COS({
    SecretId: 'AKIDcAxfLvMl8VkmaARl05ziEXfeR1P0Ujur', // 推荐使用环境变量获取；用户的 SecretId，建议使用子账号密钥，授权遵循最小权限指引，降低使用风险。子账号密钥获取可参考https://cloud.tencent.com/document/product/598/37140
    SecretKey: 'siM1a0ELkyzqY4ShrfHrKfrtnYggGsU2', // 推荐使用环境变量获取；用户的 SecretKey，建议使用子账号密钥，授权遵循最小权限指引，降低使用风险。子账号密钥获取可参考https://cloud.tencent.com/document/product/598/37140
});


export default function uploadFileToCOS(file) {
    return new Promise((resolve, reject) => {
        const params = {
            Bucket: 'dbp-1306809548',
            Region: 'ap-shanghai', // 您的存储桶所在的地区
            Key: `dbp/${file.name}`, // 上传后的文件路径和名称
            Body: file, // 要上传的文件
        };

        cos.putObject(params, (err, data) => {
            if (err) {
                reject(err);
            } else {
                resolve(data);
            }
        });
    });
};
