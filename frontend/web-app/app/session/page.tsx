import React from 'react'
import { getSession } from '../actions/AuthActions'
import Heading from '../components/Heading';

export default async function page() {

    const session = await getSession();

  return (
    <div>
        <Heading title='SEssion dashboard'/>
        <div className='bg-blue-200 border-2 border-blue-500'></div>
        <h3 className='text-lg'>Session data</h3>
        <pre>{JSON.stringify(session, null, 2)}</pre>
    </div>
  )
}
