/** @type {import('next').NextConfig} */
const nextConfig = {
    images: {
        domains: [
            'cdn.pixabay.com',
            // Добавьте другие домены, если они используются в вашем приложении
        ],
    },
    // images: {
    //     remotePatterns: [
    //         {
    //             protocol: "https",
    //             hostname: "cdn.pixabay.com"
    //         },
    //     ],
    // },
};

export default nextConfig;

