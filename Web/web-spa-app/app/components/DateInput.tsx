import { Label, TextInput } from 'flowbite-react'
import React from 'react'
import { UseControllerProps, useController } from 'react-hook-form'
import 'react-datepicker/dist/react-datepicker.css'
import DatePicker, { ReactDatePickerProps } from 'react-datepicker'

type Props = {
  label: string
  type?: string
  showLabel?: boolean
} & UseControllerProps & Partial<ReactDatePickerProps>

export default function DateInput(props: Props) {
  const {fieldState, field} = useController({...props, defaultValue: ''})

  return (
    <div className='block'>
      <DatePicker
          {...props}
          {...field}
          onChange={value => }
          placeholder={props.label}
          color={fieldState.error ? 'failure' : !fieldState.isDirty ? '' : 'success'}
          helperText={fieldState.error?.message}
        />
    </div>
  )
}

