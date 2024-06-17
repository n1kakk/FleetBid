import React from 'react'
import { getSession, getTokenWorkaround } from '../actions/AuthActions'
import Heading from '../components/Heading';

export default async function page() {

    const session = await getSession();
    const token = await getTokenWorkaround();

  return (
    <div>
        <Heading title='Session dashboard'/>
        <div className='bg-blue-200 border-2 border-blue-500'></div>
        <h3 className='text-lg'>Session data</h3>
        <pre>{JSON.stringify(session, null, 2)}</pre>
        <div className="mt-4 bg-green-200 border-2 border-green-500">
                <h3 className="text-lg">Token data</h3>
                <pre className="overflow-auto">{JSON.stringify(token, null, 2)}</pre>
            </div>
    </div>
  )
}
