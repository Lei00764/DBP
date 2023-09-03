import COS from 'cos-nodejs-sdk-v5';

const cosClient = new COS({
    SecretId: 'YOUR_SECRET_ID',
    SecretKey: 'YOUR_SECRET_KEY',
});

const uploadFileToCOS = (file) => {
    return new Promise((resolve, reject) => {
        const params = {
            Bucket: 'YOUR_BUCKET_NAME',
            Region: 'YOUR_REGION', // 您的存储桶所在的地区
            Key: `images/${file.name}`, // 上传后的文件路径和名称
            Body: file, // 要上传的文件
        };

        cosClient.putObject(params, (err, data) => {
            if (err) {
                reject(err);
            } else {
                resolve(data);
            }
        });
    });
};
