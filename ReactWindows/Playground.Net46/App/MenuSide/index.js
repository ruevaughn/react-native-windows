import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { View } from 'react-native';
import styles from './styles'

import { Pages } from '../ContentSide/constant'

import MenuButton from './MenuButton'

export default class MenuSide extends Component {
  static propTypes = {
    logger: PropTypes.func,
    menuClick: PropTypes.func,
    isFocusable: PropTypes.bool
  }

  menuClick = (page) => {
    this.props.menuClick(page)
  }

  render() {
    return (
      <View style={styles.content}>
        <MenuButton isFocusable={this.props.isFocusable} caption={Pages.MAIN} onClick={this.menuClick.bind(null, Pages.MAIN)} />
        <MenuButton isFocusable={this.props.isFocusable} caption={Pages.CONTROLS} onClick={this.menuClick.bind(null, Pages.CONTROLS)} />
        <MenuButton isFocusable={this.props.isFocusable} caption={Pages.FIXES} onClick={this.menuClick.bind(null, Pages.FIXES)} />
        <MenuButton isFocusable={this.props.isFocusable} caption={Pages.ACCESSIBILITY} onClick={this.menuClick.bind(null, Pages.ACCESSIBILITY)} />
        <MenuButton isFocusable={this.props.isFocusable} caption={'CLEAR LOG'} onClick={this.menuClick.bind(null, 'CLEAR_LOG')} />
      </View>
    )
  }
}
