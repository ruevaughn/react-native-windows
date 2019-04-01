import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { Text, View, Image } from 'react-native'
import styles from './styles'
import images from '../Images'

export default class ImageTest extends Component {
  static propTypes = {
    logger: PropTypes.func
  }

  constructor(props) {
    super(props)
  }

  render() {
    return (
      <View style={styles.content}>
        <Text style={styles.caption}>Image</Text>
        <Image style={styles.imageBox} source={images.bjnLogo} />
      </View>
    )
  }
}