'use client'
 
import React, { useState } from 'react'
 
type Props={
    imageUrl: string
}

 export default function CarImage({imageUrl}: Props) {
    const [isLoading, setLoading] = useState(true);
   return (
    <img
    src={imageUrl}
    alt='image'
    className='
        object-contain
        group-hover:opacity-75
        duration-700
        ease-in-out

    '
    width={1200}
    height={720}
    //sizes='(max-width:768px) 100vw, (max-width: 1200px) 50vw, 25vw'
    //onLoad={() => setLoading(false)}
    style={{ width: '100%', height: '100%' }}
/>
   )
 }
 